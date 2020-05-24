using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace SimpleParser
{
    class ParserZadolbaliRandom : IParser
    {
        public async Task<Info> Parse()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://ithappens.me/random";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var cellSelector = @".text";
            var cell = document.QuerySelector(cellSelector);
            return new Info(cell.TextContent);
        }
    }
}
