using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backendServer.Models
{
    public class ActividadesModel
    {
        [Key]
        private int _IdActividad;
        public int IdActividad
        {
            get { return _IdActividad; }
            set { _IdActividad = value; }
        }

        private int _IdLista;
        public int IdLista
        {
            get { return _IdLista; }
            set { _IdLista = value; }
        }

        private string _Actividad;
        public string Actividad
        {
            get { return _Actividad; }
            set { _Actividad = value; }
        }

        private DateTime _FechaInicio;
        public DateTime FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        private DateTime _FechaFin;
        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        public string cadenaFechaInicio
        {
            get { return this._FechaInicio.ToString(); }
        }
        public string cadenaFechaFin
        {
            get { return this._FechaFin.ToString(); }
        }

        private bool _Activo;
        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
    }
}