using Alarmas.Core.Helpers;
using Alarmas.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Alarmas.Core.BL.Seguridad;

namespace Alarmas.Core.BL.Seguridad
{
    public class ServicesSeguridad : ISeguridad
    {
        /// Permite crear y validar JSON Web Token.
        /// </summary>
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        /// <summary>
        /// Para la configuración del JSON Web Token.
        /// </summary>
        private readonly JWTSettings _settings;
        /// <summary>
        /// Proporciona la estructura para una llave de seguridad (firma de validación).
        /// </summary>
        private SecurityKey _issuerSigningKey;
        /// <summary>
        /// Envuelve credenciales para generar llave de seguridad (firma de validación).
        /// </summary>
        private SigningCredentials _signingCredentials;
        /// <summary>
        /// Permite envolver un objeto JSON el cual podrá ser validado como JSON Web Token. Contiene los Headers Parameter Names.
        /// </summary>
        private JwtHeader _jwtHeader;
        /// <summary>
        /// Contiene los parámetros utilizados para la validación de un JSON Web Token.
        /// </summary>
        public TokenValidationParameters Parametros { get; private set; }
        #region constructor
        /// </summary>
        /// <param name="settings">Configuración inicial para el JSON Web Token.</param>
        public ServicesSeguridad(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
            InicializarHmac();
            InicializarJWTParametros();
        }
        #endregion

        #region Metodos
        private void InicializarHmac()
        {
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }
        /// Inicializa los parametros para el objeto JWT.
        /// </summary>
        private void InicializarJWTParametros()
        {
            _jwtHeader = new JwtHeader(_signingCredentials);
            Parametros = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _settings.Issuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }
        #endregion
        /// Inicializa los objetos _issuerSigningKey y _signingCredentials con el sistema de cifrado HMAC.
        /// </summary>
        public async Task<bool> VerificaConexion()
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await (from e in conexion.CodigosAlarmas select e).CountAsync();
                if (consulta > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<CodigosAlarma>> GetListaTipoAlarmas()
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await (from e in conexion.CodigosAlarmas select e).ToListAsync();
                return consulta;               
            }
        }
    }
}
