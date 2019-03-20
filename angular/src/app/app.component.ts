import {
  Component,
  ViewContainerRef,
  Injector,
  OnInit,
  AfterViewInit,
  ChangeDetectorRef
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";

import { SignalRAspNetCoreHelper } from "@shared/helpers/SignalRAspNetCoreHelper";
import { MessagingService } from "@shared/messaging.service";
import { TokenAuthServiceProxy } from "@shared/service-proxies/service-proxies";
import { AppSessionService } from "@shared/session/app-session.service";

@Component({
  templateUrl: "./app.component.html"
})
export class AppComponent extends AppComponentBase
  implements OnInit, AfterViewInit {
  private viewContainerRef: ViewContainerRef;
  message;
  constructor(
    injector: Injector,
    private messagingService: MessagingService,
    private userAuthService: AppSessionService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    const userId = this.userAuthService.userId;
    this.messagingService.requestPermission(userId);
    this.messagingService.receiveMessage();
    this.message = this.messagingService.currentMessage;

    SignalRAspNetCoreHelper.initSignalR();

    abp.event.on("abp.notifications.received", userNotification => {
      debugger;
      abp.notifications.showUiNotifyForUserNotification(userNotification);

      //Desktop notification
      Push.create("AbpZeroTemplate", {
        body: userNotification.notification.data.message,
        icon: abp.appPath + "assets/app-logo-small.png",
        timeout: 6000,
        onClick: function() {
          window.focus();
          this.close();
        }
      });
    });
  }

  ngAfterViewInit(): void {
    $.AdminBSB.activateAll();
    $.AdminBSB.activateDemo();
  }

  onResize(event) {
    // exported from $.AdminBSB.activateAll
    $.AdminBSB.leftSideBar.setMenuHeight();
    $.AdminBSB.leftSideBar.checkStatuForResize(false);

    // exported from $.AdminBSB.activateDemo
    $.AdminBSB.demo.setSkinListHeightAndScroll();
    $.AdminBSB.demo.setSettingListHeightAndScroll();
  }
}
