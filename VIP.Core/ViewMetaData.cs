using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace VIP.Core
{
    public abstract class ViewMetaData<T> : DataViewMetaData<T>
    {
        public override void Init()
        {
            base.Init();
        }
        public override void OnInited()
        {
            base.OnInited();
            this.HtmlTags.Each(m =>
            {
                m.Value.Grid.Searchable = false;
                m.Value.Grid.Visiable = false;
            });
        }
    }
}
