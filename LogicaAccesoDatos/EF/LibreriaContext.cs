﻿using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext : DbContext
    {
        private IConfiguration _config; 
        public LibreriaContext(IConfiguration config) { 
            _config = config;
        }


        //tabla para la gestion de roles de usuarios 
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Totem> Totems { get; set; }
        public DbSet<Medico> Medicos{ get; set; }


        //tabla para el manejo de los accesos al totem
        public DbSet<AccesoTotem> AccesosTotem { get; set; }

        //tablas para el manejo de las notificaciones
        public DbSet<DispositivoNotificacion> Dispositivos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<ParametrosNotificaciones> ParametrosRecordatorios { get; set; }
        
  
        //tablas para el entrenamiento y manejo del chatbot
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<RespuestaEquivocada> RespuestasEquivocadas { get; set; }
        public DbSet<PreguntaFrec> PreguntasFrec { get; set; }
        public DbSet<CategoriaPregunta> CategoriasPregunta { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //precarga del totem
            var totemInstance = Totem.Instance;
            modelBuilder.Entity<Totem>().HasData(new Totem
            {
                Id = 1,
                Nombre = totemInstance.Nombre,
                NombreUsuario = totemInstance.NombreUsuario,
                Contrasenia = totemInstance.Contrasenia
            });
            //precarga del medico
            var medicoInstance = Medico.Instance;
            modelBuilder.Entity<Medico>().HasData(new Medico
            {
                Id = 2,
                Nombre = medicoInstance.Nombre,
                NombreUsuario = medicoInstance.NombreUsuario,
                Contrasenia = medicoInstance.Contrasenia
            });
            //precarga del Administrador
            modelBuilder.Entity<Administrador>().HasData(new Administrador
            {
                Id = 3,
                Nombre = "Octavio",
                NombreUsuario = "Admin1",
                Contrasenia = "Admin123"
            });

            //precarga del Recepcionistas
            modelBuilder.Entity<Recepcionista>().HasData(new Recepcionista
            {
                Id = 4,
                Nombre = "Maria",
                NombreUsuario = "Maria",
                Contrasenia = "Maria123"
            });
            modelBuilder.Entity<Recepcionista>().HasData(new Recepcionista
            {
                Id = 5,
                Nombre = "Laura",
                NombreUsuario = "Laura",
                Contrasenia = "Laura123"
            });

            //precarga de parametros notificacion
            var parametrosInstance = ParametrosNotificaciones.GetInstancia();
            modelBuilder.Entity<ParametrosNotificaciones>().HasData(new ParametrosNotificaciones
            {
                Id = 1,
                RecordatoriosEncendidos = parametrosInstance.RecordatoriosEncendidos,
                CadaCuantoEnviarRecordatorio = parametrosInstance.CadaCuantoEnviarRecordatorio
            });

            //precarga de categorias de preguntas
            modelBuilder.Entity<CategoriaPregunta>().HasData(
            new CategoriaPregunta
            {
                Id = 1,
                Respuesta = "Para la prueba de ingreso debes llevar la cédula.",
                Categoria = "prueba_ingreso",
                Descripcion = "Preguntas relacionadas al protocolo de ingreso"
            }, 
            new CategoriaPregunta
            {
                Id = 2,
                Respuesta = "El niño/adolescente que va a ser atendido, debe concurrir obligatoriamente con uno de sus tutores legales a cargo, o con la persona que ese tutor autorice en la entrevista de recepción que realizamos cuando ingresó al Centro. \r\nEn casos específicos de adolescentes podría evaluarse, en ese caso debería consultar con Coordinación de Agenda.\r\nEn caso de querer asistir con un acompañante mas, se permite (por ejemplo, hermanos).\r\nMientras el niño/adolescente se este atendiendo, el tutor debe permanecer en el centro, aunque no siempre ingrese a las terapias.  Las atenciones pueden ir desde 30, 45, 60, 90 o 120 minutos dependiendo de la actividad que tengas coordinada (para saber cuanto dura su tratamiento, escriba el nombre del mismo en el chat, por ejemplo, fisitría).",
                Categoria = "acompaniante",
                Descripcion = "Preguntas relacionadas a como/quien debe acompañar al paciente"
            },
            new CategoriaPregunta
            {
                Id = 3,
                Respuesta = "Disponemos de una cafetería, aquí podrá comprar comida, o traer la suya y comerla aquí. Tenemos microondas donde podrá calentarla. En caso de cualquier consulta, los voluntarios presentes en el centro, podrán ayudarle.",
                Categoria = "comida",
                Descripcion = "Preguntas relacionadas a las diferentes opciones de alimentos a las que pueden acceder los pacientes y/o familias en el centro"
            },
            new CategoriaPregunta
            {
                Id = 4,
                Respuesta = "El centro de la fundación Teletón ubicado en Montevideo, se encuentra en Carlos Brussa 2854, en el Barrio Prado. Y el centro Teletón de la ciudad de Fray Bentos, se encuentra en la dirección Zorrilla de San Martín 1484.",
                Categoria = "ubicacion",
                Descripcion = "Preguntas que solicitan la dirección de la Teletón"
            },
            new CategoriaPregunta
            {
                Id = 5,
                Respuesta = "En caso de donaciones, o devolver algún equipamiento, primero deberá comunicarse con el número de coordinación: 09*******. Si estas en el interior del país, puede enviarlo por las distintas agencias de transporte (DAC, Correo Uruguayo, etc), y por el tema del costo del envío, se charla con la coordinación y se evalúa. Y en caso de estar en Montevideo, y no tener medio de transporte, también se charla con coordinación.",
                Categoria = "donacion",
                Descripcion = "Preguntas relacionadas al protocolo de donaciones"
            },
            new CategoriaPregunta
            {
                Id = 6,
                Respuesta = "Los materiales que deben llevar el niño/adolescente varían según su tratamiento del día, para mas información escriba el nombre de su tratamiento y le enviaremos mas información.",
                Categoria = "materiales_generales",
                Descripcion = "Preguntas relacionadas a con que materiales básicos deben contar a la hora de presentarse a cualquier cita medica"
            },
            new CategoriaPregunta
            {
                Id = 7,
                Respuesta = "Las alcancías se comienzan a entregar aproximadamente un mes antes del comienzo del Programa Teletón. Todos los usuarios tienen derecho a llevar 1 alcancía, presentando la cédula en el área de voluntariado. ubicada en el Centro Teletón. Si necesitas más de 1 alcancía, en el área de voluntariado le podrán dar más información para gestionarla.",
                Categoria = "alcancias",
                Descripcion = "Preguntas relacionadas a las alcancías"
            },
            
            new CategoriaPregunta
            {
                Id = 9,
                Respuesta = "Enseguida le enviamos indicaciones",
                Categoria = "transporte",
                Descripcion = "Preguntas que solicitan direcciones para llegar al centro Teletón"
            },
            new CategoriaPregunta
            {
                Id = 10,
                Respuesta = "Enseguida le enviaremos la información sobre sus cita",
                Categoria = "cita",
                Descripcion = "Preguntas que solicitan informacion de sus citas"
            },
            new CategoriaPregunta
            {
                Id = 11,
                Respuesta = "Enseguida le enviaremos la información de la solicitud de traslado",
                Categoria = "solicitud_traslado",
                Descripcion = "Preguntas relacionadas al protocolo de de solicitudes de traslado"
            },
            new CategoriaPregunta
            {
                Id = 12,
                Respuesta = "Enseguida le enviaremos la información de la solicitud del tratamiento",
                Categoria = "tratamiento_info",
                Descripcion = "Preguntas que solicitan informacion de los diferentes tratamientos"
            }
            );
            modelBuilder.Entity<PreguntaFrec>().HasData(
            new PreguntaFrec
            {
                Id = 1,
                Pregunta = "¿Quienes deben acompañar al niño el día de la evaluación?",
                CategoriaPreguntaId = 2,
                MostrarEnTotem = true

            },
            new PreguntaFrec
            {
                Id = 2,
                Pregunta = "¿Que es la evaluación de ingreso?",
                CategoriaPreguntaId = 1,
                MostrarEnTotem = true
            },
            new PreguntaFrec
            {
                Id = 3,
                Pregunta = "¿Es posible comer en el centro?",
                CategoriaPreguntaId = 3,
                MostrarEnTotem = true
            },
            new PreguntaFrec
            {
                Id = 4,
                Pregunta = "¿Dónde se ubica el centro Teletón?",
                CategoriaPreguntaId = 4,
                MostrarEnTotem = true
            },
            new PreguntaFrec
            {
                Id = 5,
                Pregunta = "¿Cómo hago para donar?",
                CategoriaPreguntaId = 5,
                MostrarEnTotem = true
            },
            new PreguntaFrec
            {
                Id = 6,
                Pregunta = "¿Qué cosas necesita llevar el responsable del niño a la cita?",
                CategoriaPreguntaId = 6,
                MostrarEnTotem = true
            },
             new PreguntaFrec
             {
                 Id = 7,
                 Pregunta = "¿Cuál es el protocolo para las alcancías?",
                 CategoriaPreguntaId = 7,
                 MostrarEnTotem = true
             }
            );
            modelBuilder.Entity<Encuesta>().HasData(
    new Encuesta
    {
        Id = 1,
        Fecha = new DateTime(2024, 8, 20),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 4,
        Comentarios = "Excelente atención, pero la aplicación podría ser más intuitiva."
    },
    new Encuesta
    {
        Id = 2,
        Fecha = new DateTime(2024, 8, 21),
        SatisfaccionGeneral = 3,
        SatisfaccionRecepcion = 3,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 5,
        Comentarios = "Buen servicio, pero la recepción fue un poco lenta."
    },
    new Encuesta
    {
        Id = 3,
        Fecha = new DateTime(2024, 8, 22),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 4,
        Comentarios = "Todo bien, la atención fue muy rápida."
    },
    new Encuesta
    {
        Id = 4,
        Fecha = new DateTime(2024, 8, 23),
        SatisfaccionGeneral = 2,
        SatisfaccionRecepcion = 2,
        SatisfaccionEstadoDelCentro = 3,
        SatisfaccionAplicacion = 4,
        Comentarios = "Podría mejorar la señalización dentro del centro."
    },
    new Encuesta
    {
        Id = 5,
        Fecha = new DateTime(2024, 8, 24),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "Sin comentarios"
    },
    new Encuesta
    {
        Id = 6,
        Fecha = new DateTime(2024, 8, 25),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 5,
        Comentarios = "Muy satisfecho con la aplicación y el servicio."
    },
    new Encuesta
    {
        Id = 7,
        Fecha = new DateTime(2024, 8, 26),
        SatisfaccionGeneral = 3,
        SatisfaccionRecepcion = 3,
        SatisfaccionEstadoDelCentro = 3,
        SatisfaccionAplicacion = 4,
        Comentarios = "Sin comentarios"
    },
    new Encuesta
    {
        Id = 8,
        Fecha = new DateTime(2024, 8, 27),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "El personal fue muy amable y atento."
    },
    new Encuesta
    {
        Id = 9,
        Fecha = new DateTime(2024, 8, 28),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 4,
        Comentarios = "Sin comentarios"
    },
    new Encuesta
    {
        Id = 10,
        Fecha = new DateTime(2024, 8, 29),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 3,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 5,
        Comentarios = "Sin comentarios"
    },
   
    new Encuesta
    {
        Id = 11,
        Fecha = new DateTime(2024, 8, 30),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "Todo estuvo perfecto, ¡gracias!"
    },
    new Encuesta
    {
        Id = 12,
        Fecha = new DateTime(2024, 8, 31),
        SatisfaccionGeneral = 3,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 3,
        SatisfaccionAplicacion = 4,
        Comentarios = "La aplicación funcionó bien, pero el estado del centro podría mejorar."
    },
    new Encuesta
    {
        Id = 13,
        Fecha = new DateTime(2024, 9, 1),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 4,
        Comentarios = "Sin comentarios"
    },
    new Encuesta
    {
        Id = 14,
        Fecha = new DateTime(2024, 9, 2),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "Una experiencia excelente en todos los aspectos."
    }, new Encuesta
    {
        Id = 15,
        Fecha = new DateTime(2024, 9, 2),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 5,
        Comentarios = "Mejoraría la señalización del centro."
    },
    new Encuesta
    {
        Id = 16,
        Fecha = new DateTime(2024, 8, 25),
        SatisfaccionGeneral = 3,
        SatisfaccionRecepcion = 3,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 4,
        Comentarios = "La aplicación fue útil, pero el tiempo de espera en recepción fue largo."
    },
    new Encuesta
    {
        Id = 17,
        Fecha = new DateTime(2024, 8, 24),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "Una experiencia muy positiva, ¡gracias al equipo!"
    },
    new Encuesta
    {
        Id = 18,
        Fecha = new DateTime(2024, 8, 28),
        SatisfaccionGeneral = 4,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 3,
        SatisfaccionAplicacion = 4,
        Comentarios = "La atención fue buena, aunque el centro estaba un poco desordenado."
    },
    new Encuesta
    {
        Id = 19,
        Fecha = new DateTime(2024, 8, 27),
        SatisfaccionGeneral = 3,
        SatisfaccionRecepcion = 4,
        SatisfaccionEstadoDelCentro = 4,
        SatisfaccionAplicacion = 5,
        Comentarios = "La aplicación funcionó perfectamente, pero la atención podría mejorar."
    },
    new Encuesta
    {
        Id = 20,
        Fecha = new DateTime(2024, 8, 26),
        SatisfaccionGeneral = 5,
        SatisfaccionRecepcion = 5,
        SatisfaccionEstadoDelCentro = 5,
        SatisfaccionAplicacion = 5,
        Comentarios = "Todo fue excelente, la atención fue rápida y eficiente."
    }
);


            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());//aca valido que no se repitan nombres de usuario
            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
            _config["ConnectionStrings:LocalBD"]
            );
        }


    }
}

