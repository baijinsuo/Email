using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EmailDemo.EmailHelper;

namespace EmailDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化
            EmailHelper email = new EmailHelper(Consts.FROM_EMAIL_ADDRESS, Consts.FROM_EMAIL_AUTHORIZATION_CODE, EmailType.QQ_PERSONAL_EMIAL);
            email.AddNewAttachment(@"C:\Users\zz\Desktop\01.jpg");

            for (int i = 0; i < 5; i++)
            {
                SendEmail(email);
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 给指定的邮箱发送邮件
        /// </summary>
        /// <param name="email"></param>
        public static void Send(EmailHelper email)
        {

            bool state = email.Send("收件人邮箱地址", "Hello World", "测试发送邮件");
            if (state)
                Console.WriteLine("发送成功");
            else
                Console.WriteLine("发送失败");
        }

        /// <summary>
        /// 群发邮件
        /// </summary>
        /// <param name="email"></param>
        public static void SendEmail(EmailHelper email)
        {
            for (int i = 0; i < 5; i++)
                email.ToEmailList.Add("收件人邮箱地址");

            bool state = email.Send("Hello World", "测试发送邮件");
            if (state)
                Console.WriteLine("发送成功");
            else
                Console.WriteLine("发送失败");
        }
    }
}
