using cozaStore.DataAccessLayer;
using cozaStore.Models;
using System;
using System.Collections.Generic;
using System.Transactions;
namespace cozaStore.BusinessLogicLayer
{
    public class CheckOutServices : ICheckOutServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericReposistory<Order> _orderReposistory;
        private readonly IGenericReposistory<OrderDetail> _orderDetailReposistory;
        private readonly IGenericReposistory<Product> _productReposistory;

        public CheckOutServices(IUnitOfWork unitOfWork, IGenericReposistory<Order> orderReposistory, IGenericReposistory<OrderDetail> orderDetailReposistory, IGenericReposistory<Product> productReposistory)
        {
            _unitOfWork = unitOfWork;
            _orderReposistory = orderReposistory;
            _orderDetailReposistory = orderDetailReposistory;
            _productReposistory = productReposistory;
        }
        public void CheckOut(Order order, List<OrderDetail> orderDetails)
        {
            using(var transaction = new TransactionScope())
            {
                order.CreateDate = DateTime.Now;
                order.ShippedDate = DateTime.Now.AddDays(3);
                _orderReposistory.Add(order);
                foreach (var orderDetail in orderDetails)
                {
                    var product = _productReposistory.GetById(orderDetail.ProductID);
                    product.Quantity -= orderDetail.Quantity;
                    _productReposistory.Update(product);
                    orderDetail.Order = order;
                    _orderDetailReposistory.Add(orderDetail);
                }
                _unitOfWork.Commit();
                transaction.Complete();
            }
        }
    }
}
