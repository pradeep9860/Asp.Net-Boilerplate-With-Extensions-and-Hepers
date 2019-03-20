import { NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AbpHttpInterceptor } from "@abp/abpHttpInterceptor";

import * as ApiServiceProxies from "./service-proxies";
import { MessagingService } from "@shared/messaging.service";

@NgModule({
  providers: [
    ApiServiceProxies.RoleServiceProxy,
    ApiServiceProxies.SessionServiceProxy,
    ApiServiceProxies.TenantServiceProxy,
    ApiServiceProxies.UserServiceProxy,
    ApiServiceProxies.TokenAuthServiceProxy,
    ApiServiceProxies.AccountServiceProxy,
    ApiServiceProxies.ConfigurationServiceProxy,
    ApiServiceProxies.DeviceServiceProxy,
    MessagingService,
    { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
  ]
})
export class ServiceProxyModule {}
