﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Configuration;
using System.Threading;
using System.Globalization;
namespace Product_Monograph
{
    public partial class ProdMonoFr : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            lblError.InnerText = "";
            if (!Page.IsPostBack)
            {
                //all pages
                if (Session["TemplateVersion"] != null)
                {
                    lblSelectTemplate.Visible = false;
                    ddlTemplate.Disabled = true;
                    ddlTemplate.Value = Session["TemplateVersion"].ToString();
                    btnLoadTemplate.Visible = false;

                }

                //only for landing page
                if (Request.Url.ToString().ToLower().Contains("pmform"))
                {
                    ddlTemplate.Disabled = false;
                    btnLoadTemplate.Visible = true;
                }
            }
            lblTitleForm.Text = Resources.Resource.TitleForm;
        }
        protected void RequestLanguageChange_Click(object sender, EventArgs e)
        {
            LinkButton senderLink = sender as LinkButton;

            //store requested language as new culture in the session
            Session["SelectedLanguage"] = senderLink.CommandArgument;

            if (Session["SelectedLanguage"].ToString().Contains("en"))
            {
                Session["masterpage"] = "ProdMono.master";
            }
            else
            {
                Session["masterpage"] = "ProdMonoFr.master";
            }
            //reload last requested page with new culture
            Server.Transfer(Request.Path);
        }

        protected void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            if (ddlTemplate.Value == "Select")
            {
                lblError.InnerText = "Please select a template";
                return;
            }

            Session["TemplateVersion"] = ddlTemplate.Value;

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode rootnode = doc.CreateElement("ProductMonographTemplate");
            doc.AppendChild(rootnode);

            XmlNode xnode = doc.CreateElement("TemplateVersion");
            xnode.AppendChild(doc.CreateTextNode(Session["TemplateVersion"].ToString()));
            rootnode.AppendChild(xnode);

            //helpers.Processes.XMLDraft = doc;
            Session["draft"] = doc;

            Response.Redirect("Coverpage.aspx");
        }
    }
}

// <nav role="navigation" id="wb-sm-hc-prodmono" class="wb-menu visible-md visible-lg" data-trgt="mb-pnl">
//            <div class="container nvbar">
//                <div class="row">
//                    <ul class="list-inline menu">
//                        <li><a href="PMForm.aspx">Home</a></li>
//                        <li><a href="Coverpage.aspx">Cover</a></li>
//                        <li><a href="PartOne.aspx">Part 1</a></li>
//                        <li><a href="PartTwo.aspx">Part 2</a></li>
//                        <li><a href="PartThree.aspx">Part 3</a></li>
//                    </ul>
//                </div>
//            </div>
//        </nav> 


//<nav role="navigation" id="wb-sm-hc-prodmonobottom" class="wb-menu visible-md visible-lg" data-trgt="mb-pnl">
//            <div class="container nvbar">
//            <div class="row">
//                <ul class="list-inline menu">
//                    <li><a href="Coverpage.aspx">Cover</a></li>
//                    <li><a href="PartOne.aspx">Part 1</a></li>
//                    <li><a href="PartTwo.aspx">Part 2</a></li>
//                    <li><a href="PartThree.aspx">Part 3</a></li>
//                </ul>
//            </div>
//        </div>
//    </nav> 
