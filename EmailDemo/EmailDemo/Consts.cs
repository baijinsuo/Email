using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    public class Consts
    {
        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        public static readonly string FROM_EMAIL_ADDRESS = "邮箱账号";//xxxx.@qq.com

        /// <summary>
        /// 发件人邮箱授权码
        /// 发件人授权码通常获取方式为：
        /// 进入邮箱设置界面 ----> 账户----> 找到POP3/SMTP/IMAP设置 -> 开启POP3/SMTP/IMAP服务 -> 生成客户端授权码
        /// </summary>
        public static readonly string FROM_EMAIL_AUTHORIZATION_CODE = "hhhhhhhhhhhhhhhhh"; //授权码

    }
}
