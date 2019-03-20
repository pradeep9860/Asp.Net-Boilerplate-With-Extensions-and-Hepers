
using Abp.Dependency;
using Abp.Domain.Uow;
using Application.Helpers.MailHelper;
using Application.Helpers.MailHelper.Models;
using Application.Helpers.PushNotfication;
using Application.Helpers.PushNotification.Dto;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions.Hangfire.Jobs.Scheduler
{
    public class MyTestScheduler
    {
        private IMailHelper _mailHelper;
        private IPushNotificationHelper _pushNotificationHelper;
        public MyTestScheduler(IMailHelper mailHelper, IPushNotificationHelper pushNotificationHelper)
        {
            _mailHelper = mailHelper;
            _pushNotificationHelper = pushNotificationHelper;
        }

        public void Run()
        {
            //#Fire-and-forget
            //This is the main background job type, persistent message queues are used to handle it. Once you create a fire-and-forget job, it is saved to its queue ("default" by default, but multiple queues supported). The queue is listened by a couple of dedicated workers that fetch a job and perform it.
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget"));

            //#Delayed
            //If you want to delay the method invocation for a certain type, call the following method. After the given delay the job will be put to its queue and invoked as a regular fire-and-forget job.
            BackgroundJob.Schedule(() => Console.WriteLine("Delayed"), TimeSpan.FromSeconds(1));

            //#Recurring
            //To call a method on a recurrent basis (hourly, daily, etc), use the RecurringJob class. You are able to specify the schedule using CRON expressions to handle more complex scenarios.
            //RecurringJob.AddOrUpdate(() => sendMail(), Cron.Minutely);

            RecurringJob.AddOrUpdate(() => sendNotification(), Cron.Minutely);


            //#Continuations
            //Continuations allow you to define complex workflows by chaining multiple background jobs together.
            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello, "));
            BackgroundJob.ContinueWith(id, () => Console.WriteLine("world!"));
        }


        public void sendMail()
        {
            var mailModle = new EmailModel<Message>();

            mailModle.Data = new Message { Content = "My message" };

            mailModle.To = "thapaliyapradeep@gmail.com";
            mailModle.Subject = "This is test sub";

            _mailHelper.SendEmail(mailModle, "Views/Email/Email.cshtml");
        }


        [UnitOfWork]
        public virtual void sendNotification()
        {
            var userList = new List<long>();
            userList.Add(2);
            var notification = new NotificationJsonObj
            {
                Message = "Test Message",
                priority = "high",
                Title = "Test Title",
                to = "",
                notification =new NotificaionBodyDto
                {
                    UserIdList = userList,
                    body ="Test message",
                    content_available= true,
                    title = "Test Title" 
                }
            };

            _pushNotificationHelper.SendNotification(notification);
        }

        private object EmailModel<T>()
        {
            throw new NotImplementedException();
        }
    }
}
