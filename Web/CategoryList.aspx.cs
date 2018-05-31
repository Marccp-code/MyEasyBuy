﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MyEasyBuy
{
    public partial class CategoryList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckLogin();
            if (!Page.IsPostBack)
            {
              //  DataSet ds = new BLL.eb_category().GetAllList();
                List<Model.eb_category> categoryList = new BLL.eb_category().GetModelList("");
                this.rp_category.DataSource = categoryList.OrderBy(g=>g.cid).Take(10);
                this.rp_category.DataBind();
                this.lbPageIndex.Text = "1";
                this.lbSumPage.Text = Math.Ceiling(Convert.ToDouble(categoryList.Count) / Convert.ToDouble(10)).ToString();
            }
        }

        //上一页
        protected void LbuttonPrePage_Click(object sender,EventArgs e)
        {
            int pageIndex = int.Parse(this.hiddenPageIndex.Value);
            pageIndex--;
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            double sumCount = new BLL.eb_category().GetRecordCount("");  //拿到总记录数
            double pageSize = 10;  //页容量
            double sumPage = Math.Ceiling(sumCount / pageSize);  //计算总页数
            DataSet ds = new BLL.eb_category().GetListByPage("", "cid", Convert.ToInt32((pageIndex - 1) * pageSize) + 1, Convert.ToInt32(pageIndex * pageSize));
            this.rp_category.DataSource = ds.Tables[0];
            this.rp_category.DataBind();
            this.lbPageIndex.Text = pageIndex.ToString();
            this.lbSumPage.Text = sumPage.ToString();
        }

        //下一页
        protected void LbuttonNextPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse(this.hiddenPageIndex.Value);
            pageIndex++;
            //先获取总记录数
            double sumCount = new BLL.eb_category().GetRecordCount("");  //拿到总记录数
            double pageSize = 10;  //页容量
            double sumPage = Math.Ceiling(sumCount / pageSize);  //计算总页数
            if (pageIndex >= sumPage)
            {
                pageIndex = Convert.ToInt32(sumPage);
            }
            DataSet ds = new BLL.eb_category().GetListByPage("", "cid", Convert.ToInt32((pageIndex - 1) * pageSize) + 1, Convert.ToInt32(pageIndex * pageSize));
            this.rp_category.DataSource = ds.Tables[0];
            this.rp_category.DataBind();
            this.lbPageIndex.Text = pageIndex.ToString();
            this.lbSumPage.Text = sumPage.ToString();
        }
    }
}