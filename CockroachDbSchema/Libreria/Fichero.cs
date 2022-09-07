using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CockroachDbSchema.Libreria
{
    public class Fichero
    {
        private const string Filename = "CockroachDbCompare.json";

        public static async Task Escribir(Conexion conexion)
        {
            await Task.Run(async () =>
            {
                // Verificamos si existe el archivo
                if (!File.Exists($"D:\\{Filename}"))
                {
                    // Creamosel archivo
                    File.WriteAllText($"D:\\{Filename}", JsonConvert.SerializeObject(new List<Conexion>(), Formatting.Indented));
                }

                // Leemos
                List<Conexion> list = await Leer();

                // Insertamos
                list.Add(conexion);

                // Escribimos
                File.WriteAllText($"D:\\{Filename}", JsonConvert.SerializeObject(list, Formatting.Indented));
            });
        }

        public static async Task<List<Conexion>> Leer()
        {
            return await Task.Run(() =>
            {
                // Verificamos si existe el archivo
                if (!File.Exists($"D:\\{Filename}"))
                {
                    // Creamosel archivo
                    File.WriteAllText($"D:\\{Filename}", JsonConvert.SerializeObject(new List<Conexion>(), Formatting.Indented));
                }

                // Seteamos
                string json = File.ReadAllText($"D:\\{Filename}");
                List<Conexion> list = JsonConvert.DeserializeObject<List<Conexion>>(json) ?? new List<Conexion>();
                return list;
            });
        }
    }
}
