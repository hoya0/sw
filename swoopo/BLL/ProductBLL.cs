using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swoopo.Model;
using Swoopo.DAL;
using Swoopo.ExceptionHandler;

namespace Swoopo.BLL
{
    public class ProductBLL
    {
        public List<ProductEntity> getPaged(PagerCondition condition)
        {
            return new ProductDal().GetByPaged(condition);
        }

        public int Save(ProductEntity product)
        {
            ProductDal proDal = new ProductDal();
            try
            {
                if (product.ID == 0)
                {
                    return proDal.Insert(product);
                }
                else
                {
                    return proDal.Update(product, null);
                }
            }
            catch (DalException dalEx)
            {
                throw new DalException("系统异常！", dalEx);
            }
            catch (BllException bllEx)
            {
                throw new BllException("业务异常！", bllEx);
            }
            catch (Exception ex)
            {
                throw new BllException("系统异常！", ex);
            }
        }
    }//end class
}