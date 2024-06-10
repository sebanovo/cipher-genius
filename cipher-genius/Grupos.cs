using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_genius
{
    class Grupos
    {
        public string CifrarPorGrupos(string mensaje, int[] permutacion)
        {
            mensaje = mensaje.Replace(" ", "");
            var mensajeArray = mensaje.Trim().ToCharArray().ToList();
            var longitudGrupo = permutacion.Length;
            var cantidadGrupos = (int)Math.Ceiling((double)mensajeArray.Count / longitudGrupo);
            var caracteresFaltantes = longitudGrupo - (mensajeArray.Count % longitudGrupo);

            // Rellenar el mensaje con 'X' para completar el último grupo
            for (int i = 0; i < caracteresFaltantes; i++)
            {
                mensajeArray.Add('X');
            }

            var gruposCifrados = new List<string>();

            for (int i = 0; i < cantidadGrupos; i++)
            {
                var grupoCifrado = "";

                for (int j = 0; j < longitudGrupo; j++)
                {
                    var posicionMensaje = i * longitudGrupo + permutacion[j];
                    grupoCifrado += mensajeArray[posicionMensaje];
                }

                gruposCifrados.Add(grupoCifrado);
            }

            // Unir grupos
            var mensajeCifrado = string.Concat(gruposCifrados);

            return mensajeCifrado;
        }

        public string DescifrarPorGrupos(string mensajeCifrado, int[] permutacion)
        {
            mensajeCifrado = mensajeCifrado.Replace(" ", "");
            var longitudGrupo = permutacion.Length;
            var cantidadGrupos = mensajeCifrado.Length / longitudGrupo;
            var gruposDescifrados = new List<string>();

            // Iterar sobre cada grupo
            for (int i = 0; i < cantidadGrupos; i++)
            {
                var grupoDescifrado = "";

                for (int j = 0; j < longitudGrupo; j++)
                {
                    var posicionCifrado = i * longitudGrupo + Array.IndexOf(permutacion, j);
                    grupoDescifrado += mensajeCifrado[posicionCifrado];
                }

                gruposDescifrados.Add(grupoDescifrado);
            }

            var mensajeDescifrado = string.Concat(gruposDescifrados);

            return mensajeDescifrado;
        }
    }
}
