﻿using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace Easy.Modules.DataDictionary
{
    public class DataDictionaryService : ServiceBase<DataDictionaryEntity>, IDataDictionaryService
    {
        DataDictionaryRepository rep = new DataDictionaryRepository();
        public IEnumerable<DataDictionaryEntity> GetDictionaryByType(string dicType)
        {
            return Get(new Data.DataFilter().Where(string.Format("T0.DicValue<>'0' and T0.DicName='{0}'", dicType)));
        }
        public IEnumerable<string> GetDictionaryType()
        {
            return rep.GetDictionaryType();
        }
        public override void Add(DataDictionaryEntity item)
        {
            var parent = this.Get(item.Pid);
            item.DicName = parent.DicName;
            base.Add(item);
        }

        public IEnumerable<DataDictionaryEntity> GetChildren(string dicType, long id)
        {
            var dicts = this.GetDictionaryByType(dicType);
            return InitChildren(dicts, id);
        }
        private IEnumerable<DataDictionaryEntity> InitChildren(IEnumerable<DataDictionaryEntity> source, long id)
        {
            IEnumerable<DataDictionaryEntity> result = source.Where(m => m.Pid == id);
            List<DataDictionaryEntity> listResult = result.ToList();
            result.Each(m =>
            {
                listResult.AddRange(InitChildren(source, m.ID));
            });
            return listResult;
        }
    }
}
