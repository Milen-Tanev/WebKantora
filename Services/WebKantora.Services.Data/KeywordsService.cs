using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class KeywordsService: IKeywordsService
    {
        private IKeywordDbRepository keywords;
        private IUnitOfWork unitOfWork;

        public KeywordsService(IKeywordDbRepository keywords, IUnitOfWork unitOfWork)
        {
            this.keywords = keywords;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Keyword> GetAll()
        {
            var allKeywords = this.keywords.All()
                .OrderByDescending(a => a.Content);

            return allKeywords;
        }
    }
}
