using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentAppBackend.Model;
using System.Threading.Tasks;

namespace AppointmentAppBackend.Interfaces;

public interface IJWTManagerService
{
    Tokens Authenticate(Users users);
}
