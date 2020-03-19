using Caliburn.Micro;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.Models
{
    public class InformationBox
    {
        public static void ShowError(Exception e) { 
        
        }
        public static void ShowError(RpcException e)
        {
            switch (e.StatusCode)
            {
                case StatusCode.OK:
                    break;
                case StatusCode.Cancelled:
                    break;
                case StatusCode.Unknown:
                    MessageBox.Show($"Ismeretlen hiba! \nVegye fel a kapcsolatott a rendszergazdával!\n{e.Message}", "Ismeretlen hiba.", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case StatusCode.InvalidArgument:
                    break;
                case StatusCode.DeadlineExceeded:
                    break;
                case StatusCode.NotFound:
                    break;
                case StatusCode.AlreadyExists:
                    MessageBox.Show($"Hiba: {e.StatusCode} \n\nHiba üzenet: {e.Status.Detail}","Hiba az adatfelvitele során.",MessageBoxButton.OK,MessageBoxImage.Error);
                    break;
                case StatusCode.PermissionDenied:
                    break;
                case StatusCode.Unauthenticated:
                    MessageBox.Show($"Hibás felhasználónév vagy jelszó! Kérem próbálja újra.", "Hibás adatok.", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case StatusCode.ResourceExhausted:
                    break;
                case StatusCode.FailedPrecondition:
                    break;
                case StatusCode.Aborted:
                    break;
                case StatusCode.OutOfRange:
                    break;
                case StatusCode.Unimplemented:
                    break;
                case StatusCode.Internal:
                    break;
                case StatusCode.Unavailable:
                    MessageBox.Show($"A szerver nem elérhető \nKérem próbálja később.", "Adatkapcsolati hiba.", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case StatusCode.DataLoss:
                    break;
                default:
                    break;
            }
        }
        public static bool ShowWarning() {
            return true;
        }
        
    }
}
