using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace Logica.Library
{
    public class Page<T>
    {
        private List<T> _dataList;
        private Label _label;
        private static int maxReg, reg_by_page, pageCount, numPage = 1;

        public Page(List<T>dataList,Label label,int reg_by_pagep) {
            this._dataList = dataList;
            this._label = label;
            reg_by_page = reg_by_pagep;
            dataLoad();
        }
        private void dataLoad()
        {
            numPage = 1;
            maxReg=_dataList.Count;
            pageCount = (maxReg / reg_by_page);

            if((maxReg % reg_by_page) > 0)
            {
                pageCount += 1;
            }
            this._label.Text = $"Paginas 1 / {pageCount}";
        }
        public int first()
        {
            numPage = 1;
            this._label.Text= $"Paginas  {numPage}/{pageCount}";
            return numPage;
        }
        public int preview()
        {
            if (numPage > 1)
            {
                numPage -= 1;
                this._label.Text = $"Paginas  {numPage}/{pageCount}";
            }

            return numPage;
        }
        public int next()
        {
            if (numPage == pageCount)
            {
                numPage -= 1;  
            }
            if (numPage < pageCount)
            {
                numPage+= 1;
                this._label.Text = $"Paginas  {numPage}/{pageCount}";
            }
            return numPage;
        }
        public int last()
        {
            numPage = pageCount;
            this._label.Text = $"Paginas  {numPage}/{pageCount}";
            return numPage;

        }
    
    }
}
