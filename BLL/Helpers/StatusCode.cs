using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public enum CreateUserStatus
    {
        //Усе добре
        Success=0,
        //Узер з тами емейлом уже є
        DuplicateEmail=1,
        //Помилка стврення узера
        UserErrorCreate=2

    }
}
