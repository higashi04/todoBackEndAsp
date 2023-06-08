using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backendServer.Models
{
    public class ListasModel
    {
        [Key]
        private int _IdLista;
        public int IdLista
        {
            get { return _IdLista; }
            set { _IdLista = value; }
        }
        private int _IdUsuario;
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
    }
}
