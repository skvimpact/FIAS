using Fias.DataBase;
using Fias.Models;
using Fias.Repos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Fias
{
    public class Container
    {
        private readonly List<Model> models;
        private readonly IRepo<Model> repo;
        private int total;

        public Container(string connectionString, string tableName, string schema)
        {
            repo = new Repo<Model>(new MsSqlAccess(connectionString), tableName, schema);
            models = new List<Model>();
        }

        public void Add(Model model) => models.Add(model);
       
        public void Flush()
        {
            int count = models.Count;
            total += count;
            Console.WriteLine($"{DateTime.Now}. Wrote {count}({total}) records.");
            repo.AddRange(models);
            models.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            
            DateTime start = DateTime.Now;
            Console.WriteLine($"Started at {start}.");
            IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();
            int bufferSize = int.Parse(config["BufferSize"]);
            var container = new Container(config["DataBase:ConnectionString"],
                                          config["DataBase:TableName"],
                                          config["DataBase:Schema"]);
            Model model;
            StreamReader xmlStream = new StreamReader(config["File"]);
            using (XmlReader reader = XmlReader.Create(xmlStream))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (reader.HasAttributes && reader.Name == "Object")
                            {
                                count++;
                                model = new Model();
                                while (reader.MoveToNextAttribute())
                                {
                                    switch (reader.Name)
                                    {
                                        case "AOGUID": model.AOGUID = reader.Value; break;
                                        case "FORMALNAME": model.FORMALNAME = reader.Value; break;
                                        case "REGIONCODE": model.REGIONCODE = reader.Value; break;
                                        case "AUTOCODE": model.AUTOCODE = reader.Value; break;
                                        case "AREACODE": model.AREACODE = reader.Value; break;
                                        case "CITYCODE": model.CITYCODE = reader.Value; break;
                                        case "CTARCODE": model.CTARCODE = reader.Value; break;
                                        case "PLACECODE": model.PLACECODE = reader.Value; break;
                                        case "PLANCODE": model.PLANCODE = reader.Value; break;
                                        case "STREETCODE": model.STREETCODE = reader.Value; break;
                                        case "EXTRCODE": model.EXTRCODE = reader.Value; break;
                                        case "SEXTCODE": model.SEXTCODE = reader.Value; break;
                                        case "OFFNAME": model.OFFNAME = reader.Value; break;
                                        case "POSTALCODE": model.POSTALCODE = reader.Value; break;
                                        case "IFNSFL": model.IFNSFL = reader.Value; break;
                                        case "TERRIFNSFL": model.TERRIFNSFL = reader.Value; break;
                                        case "IFNSUL": model.IFNSUL = reader.Value; break;
                                        case "TERRIFNSUL": model.TERRIFNSUL = reader.Value; break;
                                        case "OKATO": model.OKATO = reader.Value; break;
                                        case "OKTMO": model.OKTMO = reader.Value; break;
                                        case "UPDATEDATE": model.UPDATEDATE = DateTime.Parse(reader.Value); break;
                                        case "SHORTNAME": model.SHORTNAME = reader.Value; break;
                                        case "AOLEVEL": model.AOLEVEL = int.Parse(reader.Value); break;
                                        case "PARENTGUID": model.PARENTGUID = reader.Value; break;
                                        case "AOID": model.AOID = reader.Value; break;
                                        case "PREVID": model.PREVID = reader.Value; break;
                                        case "NEXTID": model.NEXTID = reader.Value; break;
                                        case "CODE": model.CODE = reader.Value; break;
                                        case "PLAINCODE": model.PLAINCODE = reader.Value; break;
                                        case "ACTSTATUS": model.ACTSTATUS = int.Parse(reader.Value); break;
                                        case "CENTSTATUS": model.CENTSTATUS = int.Parse(reader.Value); break;
                                        case "OPERSTATUS": model.OPERSTATUS = int.Parse(reader.Value); break;
                                        case "CURRSTATUS": model.CURRSTATUS = int.Parse(reader.Value); break;
                                        case "STARTDATE": model.STARTDATE = DateTime.Parse(reader.Value); break;
                                        case "ENDDATE": model.ENDDATE = DateTime.Parse(reader.Value); break;
                                        case "NORMDOC": model.NORMDOC = reader.Value; break;
                                        case "LIVESTATUS": model.LIVESTATUS = byte.Parse(reader.Value); break;
                                        case "DIVTYPE": model.DIVTYPE = int.Parse(reader.Value); break;
                                        default:
                                            break;
                                    }
                                }                                
                                container.Add(model);
                                if (count % bufferSize == 0) container.Flush();                               
                                reader.MoveToElement(); // Move the reader back to the element node.
                            }
                            break;
                        case XmlNodeType.EndElement:
                            container.Flush();
                            break;
                        default:
                            break;
                    }
                }
            }            
            Console.WriteLine($"Completed at {DateTime.Now - start}.");            
        }
    }
}
