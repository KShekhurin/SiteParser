using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;


namespace SimpleParser
{
    class ParserBashRandom : IParser
    {
        public async Task<Info> Parse()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://bash.im/random";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var cellSelector = @".quote__header+.quote__body";
            var cell = document.QuerySelector(cellSelector);
            return new Info(cell.TextContent);
        }
    }
}
