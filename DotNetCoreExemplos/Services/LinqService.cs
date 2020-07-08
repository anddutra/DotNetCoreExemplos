using DotNetCoreExemplos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreExemplos.Services
{
    //https://www.caelum.com.br/apostila-csharp-orientacao-objetos/linq-e-lambda/#null
    //https://www.tutorialsteacher.com/linq/sample-linq-queries
    //https://docs.microsoft.com/pt-br/dotnet/api/system.linq.enumerable.groupby?view=netcore-3.1
    public class LinqService
    {
        public double GetSum()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Sum(s => s.Valor);
        }

        public IEnumerable<LinqModel> GetWhere(double valor)
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Where(l => l.Valor >= valor);
        }

        public IEnumerable<int> GetWhereSelect(double valor)
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Where(l => l.Valor == valor).Select(s => s.Id);
        }

        public IEnumerable<string> GetSelectDistinct()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Select(l => l.Tipo).Distinct();
        }

        public double GetMax()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Max(l => l.Valor);
        }

        public double GetMin()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Min(l => l.Valor);
        }

        public IEnumerable<LinqModel> GetUnion()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.Union(LinqModel.GetListLinqModel2());
        }

        public IEnumerable<string> GetGroupBy()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            return linqs.GroupBy(l => l.Tipo).Select(s => s.Key).ToList();
        }

        public object GetGroupBy2()
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();

            /*return linqs.GroupBy(l => l.Tipo)
                .Select(group => new { Tipo = group.Key, Valores = group.ToList() })
                .ToList();*/

            return linqs.GroupBy(l => l.Tipo, (key, group) =>
                new { Tipo = key, Valores = group.ToList() }).ToList();
        }

        public IEnumerable<LinqModel> GetLinqQuery(double valor)
        {
            IEnumerable<LinqModel> linqs = LinqModel.GetListLinqModel();
            var resultado = from c in linqs
                            where c.Valor > valor
                            orderby c.Tipo
                            select c;
            return resultado.ToList();
        }
    }
}