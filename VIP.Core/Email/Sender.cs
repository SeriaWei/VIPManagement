using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIP.Core.Email
{
    public static class Sender
    {
        public static bool IsSenderEnable = true;
        public static int SleepSecond = 20;
        public static void Start()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                IEmailHostService emailHostService = Easy.Loader.CreateInstance<IEmailHostService>();

                while (true)
                {
                    while (IsSenderEnable && emailHostService.Count(new Easy.Data.DataFilter().Where("IsEnable=true")) > 0)
                    {
                        IEmailService emailService = Easy.Loader.CreateInstance<IEmailService>();
                        Easy.Data.ConditionGroup group = new Easy.Data.ConditionGroup();
                        group.Add(new Easy.Data.Condition("Status", Easy.Data.OperatorType.Equal, 1));
                        group.Add(new Easy.Data.Condition("Status=2", Easy.Data.ConditionType.Or));
                        IEnumerable<EmailMessage> msgs = emailService.Get(new Easy.Data.DataFilter().Where(group)
                             .OrderBy("ID", Easy.Data.OrderType.Ascending));
                        if (msgs.Any())
                        {
                            EmailMessage msg = msgs.Where(m => m.Status == 2).FirstOrDefault();
                            if (msg == null)
                            {
                                msg = msgs.Where(m => m.Status == 1).FirstOrDefault();
                            }
                            if (msg != null)
                            {
                                List<EmailHost> hosts = emailHostService.Get(new Easy.Data.DataFilter().Where("IsEnable=true")).ToList();
                                if (!hosts.Any()) throw new Exception("没有可用的邮箱服务");
                                Random ran = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * 1000);
                                EmailHost host = hosts[ran.Next(0, hosts.Count)];
                                host.UseTimes++;
                                emailHostService.Update(host);
                                try
                                {
                                    new Easy.Net.Email.EmailSender().Send(new EmailContext(msg, host));
                                }
                                catch (Exception ex)
                                {
                                    Easy.Logger.Info(host.EmailAddress);
                                    Easy.Logger.Error(ex);
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(SleepSecond * 1000);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }));
        }
        public static void Stop()
        {
            IsSenderEnable = false;
        }
    }
}
