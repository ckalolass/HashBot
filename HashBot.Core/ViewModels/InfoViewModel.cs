using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBot.Core.ViewModels
{
    public class InfoViewModel : MvxViewModel
    {
        public InfoViewModel()
        {
            _LogoUri = "/Assets/logo.png";
            _PhoneUri = "/Assets/icon_phone.png";
            _EmailUri = "/Assets/icon_mail.png";
            _Text = @"Нам не стыдно за выпускаемые продукты, все они сделаны с вниманием к деталям. Пользователи это ценят, многие наши приложения попадают в топы AppStore и получают высокие оценки. 

Мы любим своих заказчиков и решаем их задачи. На письма и телефон отвечаем быстро, по праздникам и выходным, делаем работу в срок и никуда не пропадаем.
Закажите разработку сейчас! 
";
        }

        private string _LogoUri;
        public string LogoUri
        { 
            get 
            { 
                return _LogoUri; 
            } 
        }

        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
        }

        private string _PhoneUri;
        public string PhoneUri
        {
            get
            {
                return _PhoneUri;
            }
        }

        private string _EmailUri;
        public string EmailUri
        {
            get
            {
                return _EmailUri;
            }
        }
    }
}
