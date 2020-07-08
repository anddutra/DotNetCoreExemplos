using System.Collections.Generic;

namespace DotNetCoreExemplos.Models
{
    public class LinqModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }

        public static IEnumerable<LinqModel> GetListLinqModel()
        {
            List<LinqModel> linqs = new List<LinqModel>();
            LinqModel model;

            model = new LinqModel { Id = 1, Tipo = "A", Valor = 1000 };
            linqs.Add(model);
            model = new LinqModel { Id = 2, Tipo = "A", Valor = 3000 };
            linqs.Add(model);
            model = new LinqModel { Id = 3, Tipo = "A", Valor = 5000 };
            linqs.Add(model);
            model = new LinqModel { Id = 4, Tipo = "B", Valor = 2500 };
            linqs.Add(model);
            model = new LinqModel { Id = 5, Tipo = "B", Valor = 2700 };
            linqs.Add(model);
            model = new LinqModel { Id = 6, Tipo = "B", Valor = 1000 };
            linqs.Add(model);

            return linqs;
        }

        public static IEnumerable<LinqModel> GetListLinqModel2()
        {
            List<LinqModel> linqs = new List<LinqModel>();
            LinqModel model;

            model = new LinqModel { Id = 10, Tipo = "A", Valor = 10000 };
            linqs.Add(model);
            model = new LinqModel { Id = 20, Tipo = "B", Valor = 30000 };
            linqs.Add(model);

            return linqs;
        }
    }
}
