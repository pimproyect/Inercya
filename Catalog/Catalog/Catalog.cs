using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Catalog
{
    class Catalog
    {
        public static List<Categoria> _Categorias = new List<Categoria>();
        public static List<Product> _Productos = new List<Product>();
        public static List<Catalogo> _Catalogo = new List<Catalogo>();
        static void Main(string[] args)
        {
            CargaCategorias();
            CargaProductos();
            CargaCatalogo();
            ExportarJSON();
            ExportarXML();
            Console.ReadLine();
        }

        static void ExportarXML()
        {
            var serializer = new XmlSerializer(_Catalogo.GetType());
            string utf8;
            using (StringWriter writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, _Catalogo);
                utf8 = writer.ToString();
            }
            File.WriteAllText("../Catalog.xml", utf8);
            

        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        static void ExportarJSON() {
            string jsonString = JsonConvert.SerializeObject(_Catalogo);
            File.WriteAllText("../Catalog.json", jsonString);
        }

        static void CargaCatalogo()
        {
            foreach(Categoria categoria in _Categorias)
            {
                Catalogo catalogo = new Catalogo();
                catalogo.Categoria = new Categoria()
                {
                    Id = categoria.Id,
                    Name = categoria.Name,
                    Description = categoria.Description
                };
                catalogo.Products = new List<Product>();
                var productosPorCategoria = _Productos.Where(x => x.CategoryId == categoria.Id);
                foreach(Product producto in productosPorCategoria)
                {
                    catalogo.Products.Add(producto);
                }
                _Catalogo.Add(catalogo);
            }
        }

        static void CargaCategorias()
        {
            var reader = new StreamReader(File.OpenRead("../Categories.csv"));
            bool primeraLinea = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (!primeraLinea)
                {
                    Categoria categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(values[0]),
                        Name = values[1],
                        Description = values[2]
                    };

                    _Categorias.Add(categoria);
                }
                primeraLinea = false;
            }
        }

        static void CargaProductos()
        {
            var reader = new StreamReader(File.OpenRead("../Products.csv"));
            bool primeraLinea = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (!primeraLinea)
                {
                    Product producto = new Product()
                    {
                        Id = Convert.ToInt32(values[0]),
                        CategoryId = Convert.ToInt32(values[1]),
                        Name = values[2],
                        Price = Convert.ToDecimal(values[3])
                    };

                    _Productos.Add(producto);
                }
                primeraLinea = false;
            }
        }
    }
}
