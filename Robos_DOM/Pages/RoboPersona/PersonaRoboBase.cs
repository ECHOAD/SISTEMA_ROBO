using DataLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Radzen;
using Robos_DOM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Robos_DOM.Pages.RoboPersona
{
    public class PersonaRoboBase : ComponentBase
    {
        [Inject]
        private IDataAccess _data { get; set; }

        [Inject]
        private IConfiguration _config { get; set; }

        protected List<PersonaRoboModel> lst_reportes { get; set; }
        protected PersonaRoboModel OPersona = new();

        protected bool ModoEditar = false;


        protected override async Task OnInitializedAsync()
        {
            var query = "SELECT * FROM ROBOS_PERSONA";
            lst_reportes = await _data.LoadData<PersonaRoboModel, dynamic>(query, new { }, _config.GetConnectionString("default"));
            OPersona = new();



        }


        protected async Task SaveData()
        {
            var query = "sp_ReportesRobo @Cedula,@Nombre,@Apellido,@Fecha,@Valor,@Descripcion,@Direccion,@Latitud,@Longitud";
            await _data.SaveData<PersonaRoboModel>(query, OPersona, _config.GetConnectionString("default"));
            await OnInitializedAsync();

        }

        protected async Task LoadEditar(int Id)
        {
            string query = "SELECT * FROM ROBOS_PERSONA WHERE Id = @Id";

            OPersona = await _data.LoadObject<PersonaRoboModel, dynamic>(query, new { Id = Id }, _config.GetConnectionString("default"));

            ModoEditar = true;
        }

        protected async Task UpdateData()
        {
            string query = $"UPDATE ROBOS_PERSONA SET Nombre = @Nombre, Apellido = @Apellido , " +
                $"Descripcion= @Descripcion, Valor=@Valor, Fecha=@Fecha, Direccion=@Direccion, " +
                $"Latitud=@Latitud, Longitud=@Longitud Where Id= @Id";


            await _data.SaveData<PersonaRoboModel>(query, OPersona, _config.GetConnectionString("default"));
            await OnInitializedAsync();

            ModoEditar = false;
        }

        protected async Task ExportarDato(int Id)
        {
            string query = "SELECT * FROM ROBOS_PERSONA WHERE Id = @Id";

         

            OPersona = await _data.LoadObject<PersonaRoboModel, dynamic>(query, new { Id = Id }, _config.GetConnectionString("default"));
            
            
            StringBuilder sb = new StringBuilder();

            sb.Append("<h1>Nombre</h1>");
            sb.Append($"<h3>{OPersona.Nombre}</h3>");
            sb.Append("<h1>Apellido</h1>");
            sb.Append($"<h3>{OPersona.Apellido}</h3>");
            sb.Append("<h1>Telefono</h1>");
            sb.Append($"<h5>{OPersona.Descripcion}</h5>");
            sb.Append("<h1>Direccion</h1>");
            sb.Append($"<h5>{OPersona.Direccion}</h5>");

            System.IO.File.WriteAllText(@"..\DatosPersonas.html", sb.ToString());

        }

    }

}

