﻿using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class ModifyTelephelyViewModel : TorzsDataView<Telephely>, IHandle<Telephely>
    {

        private Telephely _telephely;

        public Telephely Telephely
        {
            get { return _telephely; }
            set { _telephely = value; }
        }

        private string _telephelyNeve = "";

        public string TelephelyNeve
        {
            get { return _telephelyNeve; }
            set { _telephelyNeve = value;
                NotifyOfPropertyChange(()=>TelephelyNeve);
            }
        }

        public override void DoAction()
        {
            Telephely.Name = TelephelyNeve;
            bool success = serverHelper.ModifyTelephely(Telephely);
            eventAggregator.PublishOnUIThread(Telephely);
            TryClose();
        }
        public bool CanDoAction
        {
            get
            {
                if (TelephelyNeve.Length > 4 && TelephelyNeve.Length < 100) return true;
                else return false;
            }
        }
        public void Handle(Telephely message)
        {
            Telephely = new Telephely(message);
            TelephelyNeve = message.Name;
        }
    }
}
