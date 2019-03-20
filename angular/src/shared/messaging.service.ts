import { Injectable } from "@angular/core";
import { AngularFireDatabase } from "@angular/fire/database";
import { AngularFireAuth } from "@angular/fire/auth";
import { AngularFireMessaging } from "@angular/fire/messaging";
import { mergeMapTo } from "rxjs/operators";
import { take } from "rxjs/operators";
import { BehaviorSubject } from "rxjs";
import {
  DeviceServiceProxy,
  DeviceInfoDto
} from "./service-proxies/service-proxies";
import { DeviceDetectorService } from "ngx-device-detector";

@Injectable()
export class MessagingService {
  currentMessage = new BehaviorSubject(null);

  constructor(
    private angularFireDB: AngularFireDatabase,
    private angularFireAuth: AngularFireAuth,
    private angularFireMessaging: AngularFireMessaging,
    private deviceService: DeviceServiceProxy,
    private deviceDetectorService: DeviceDetectorService
  ) {
    this.angularFireMessaging.messaging.subscribe(_messaging => {
      _messaging.onMessage = _messaging.onMessage.bind(_messaging);
      _messaging.onTokenRefresh = _messaging.onTokenRefresh.bind(_messaging);
    });
  }

  /**
   * update token in firebase database
   *
   * @param userId userId as a key
   * @param token token as a value
   */
  updateToken(userId, token) {
    let deviceInfo = this.deviceDetectorService.getDeviceInfo();
    var deviceInfoModel: DeviceInfoDto = Object.assign(
      {},
      new DeviceInfoDto(),
      {
        hardwareModel: `${deviceInfo.browser}_${deviceInfo.os}_${
          deviceInfo.os_version
        }_${deviceInfo.browser_version}`,
        platformType: "web",
        platformVersion: `${deviceInfo.browser_version}`,
        regID: token,
        uniqueDeviceId: `${userId}_XXXX_WEB_2.0_${deviceInfo.browser}_${
          deviceInfo.os
        }_${deviceInfo.os_version}_${deviceInfo.browser_version}`
      }
    );

    //unsubscribe old token if exists for that user
    if (localStorage.getItem(userId)) {
      debugger;
      var di = JSON.parse(localStorage.getItem(userId));
      var model: DeviceInfoDto = Object.assign(di);
      this.deviceService.unsubscribe(model).subscribe(res => {
        localStorage.removeItem(userId);

        //subscribe new token
        this.deviceService.subscribe(deviceInfoModel).subscribe(res => {
          localStorage.setItem(userId, JSON.stringify(res));
        });
      });
    } else {
      //subscribe new token
      this.deviceService.subscribe(deviceInfoModel).subscribe(res => {
        localStorage.setItem(userId, JSON.stringify(res));
      });
    }

    // we can change this function to request our backend service
    // this.angularFireAuth.authState.pipe(take(1)).subscribe(() => {
    //   const data = {};
    //   data[userId] = token;
    //   this.angularFireDB.object("fcmTokens/").update(data);
    // });
  }

  /**
   * request permission for notification from firebase cloud messaging
   *
   * @param userId userId
   */
  requestPermission(userId) {
    this.angularFireMessaging.requestToken.subscribe(
      token => {
        console.log(token);
        this.updateToken(userId, token);
      },
      err => {
        console.error("Unable to get permission to notify.", err);
      }
    );
  }

  /**
   * hook method when new notification received in foreground
   */
  receiveMessage() {
    this.angularFireMessaging.messages.subscribe(payload => {
      console.log("new message received. ", payload);
      this.currentMessage.next(payload);
    });
  }
}
