using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    public class PagerCondition
    {
        private string _selectField = "*";
        /// <summary>
        /// 查询列
        /// </summary>
        public string SelectFields
        {
            get
            {
                return _selectField;
            }
            set
            {
                _selectField = value;
            }
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }

        private int _pageSize = 10;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        private byte _doCount = 0;
        /// <summary>
        /// 返回总页数
        /// </summary>
        public byte doCount
        {
            get
            {
                return _doCount;
            }
            set
            {
                _doCount = value;
            }
        }

        private byte _sortType = 1;
        /// <summary>
        /// 排序类型，非0降序
        /// </summary>
        public byte SortType
        {
            get
            {
                return _sortType;
            }
            set
            {
                _sortType = value;
            }
        }

        private string _strWhere = string.Empty;
        public string StrWhere
        {
            get
            {
                return _strWhere;
            }
            set
            {
                _strWhere = value;
            }
        }

    }
}
