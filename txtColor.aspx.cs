using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Threading;

namespace txtbox.Form
{
    public partial class txtColor : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        int cnt=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dvPoppup.Style.Add("display", "none");
                bindgrid();
                //clear();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("Data Source=occweb05;initial catalog=Anupam ; User ID=sa;Password=odpserver550810998@;");

            SqlCommand cmd = new SqlCommand("INSERT INTO greenGrid (Name,  Address, MobileNo,StatusCode) VALUES ('" + name.Text + "','" + Address.Text + "','" + MobileNo.Text + "',0)", cn);

            cn.Open();
            cmd.ExecuteNonQuery();
            msg.Text = " Data Save";
            cn.Close();
        }
        public void bindgrid()
        {

            SqlConnection con = new SqlConnection("Data Source=occweb05;initial catalog=Anupam ; User ID=sa;Password=odpserver550810998@;");
            SqlDataAdapter da = new SqlDataAdapter("Select * from greenGrid", con);
            DataTable dt = new DataTable();
            //clear();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                cnt = dt.Rows.Count;
                grvStudent.DataSource = dt;
                grvStudent.DataBind();
                ViewState["VSStudent"] = dt;
            }
            else
            {
                Label1.Text =cnt.ToString();
            }
           

          
        }

        protected void Clear_Click(object sender, EventArgs e)
        {

        }
        protected void grvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[3].Text) == 1)
                {
                    e.Row.Cells[3].Text = "Completed ";
                    e.Row.Cells[3].ForeColor = Color.LightGreen;
                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text) == 0)
                {
                    e.Row.Cells[3].Text = "Pending ";
                    e.Row.Cells[3].ForeColor = Color.Red;
                }
                else
                {
                    e.Row.Cells[3].Text = "Namaste";
                    e.Row.Cells[3].ForeColor = Color.White;
                }
            }
        }
        protected void grvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Show")
            {
                Name1.Text = (grvStudent.Rows[RowIndex].FindControl("lblName") as Label).Text.ToString();
                Address1.Text = ((Label)grvStudent.Rows[RowIndex].FindControl("lblAddress")).Text.ToString();
                MobileNo1.Text = ((Label)grvStudent.Rows[RowIndex].FindControl("lblmobile")).Text.ToString();
                //Status.Text = ((Label)grvStudent.Rows[RowIndex].FindControl("lblCode")).Text.ToString();
                Id.Value = ((HiddenField)grvStudent.Rows[RowIndex].FindControl("Id")).Value;
                Status.Text = ((HiddenField)grvStudent.Rows[RowIndex].FindControl("lblStatus")).Value;
                dvPoppup.Style.Add("display", "block");
                //dvPoppup.Visible = true;
            }
            else if(e.CommandName=="Del")
            {
                int Rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                SqlConnection con = new SqlConnection("Data Source=occweb05;initial catalog=Anupam ; User ID=sa;Password=odpserver550810998@;");
                Id.Value = ((HiddenField)grvStudent.Rows[Rowindex].FindControl("Id")).Value;
                SqlDataAdapter da = new SqlDataAdapter("Delete from greenGrid where Id=" + Id.Value + "",con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showAlert();", true);
                Thread.Sleep(1500);
                msg.Text = "Data Delete";
                grvStudent.DataSource = dt;
                grvStudent.DataBind();
            }
            
        }

        protected void Modelbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=occweb05;initial catalog=Anupam ; User ID=sa;Password=odpserver550810998@;");
            SqlDataAdapter da = new SqlDataAdapter("update greenGrid set Name= '" + Name1.Text + "', Address='" + Address1.Text + "' , MobileNo='" + MobileNo1.Text + "',StatusCode='"+Status.Text+"' where Id=" + Id.Value, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Label1.Text = "Data Update";
            grvStudent.DataSource = dt;
            grvStudent.DataBind();

        }

        protected void model_Click(object sender, EventArgs e)
        {
         
        }

        protected void checkBoxId_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in grvStudent.Rows)
            //{
            //    CheckBox Chechbox = (CheckBox)row.FindControl("checkBoxId");
            //    Button button = (Button)row.FindControl("DeleteboxId");
            //    FileUpload fileupload = (FileUpload)row.FindControl("FileUpload1");
            //    if (Chechbox.Checked == true)
            //    {
            //        button.Enabled = true;
            //        fileupload.Enabled = true;
            //    }
            //    if (Chechbox.Checked == false)
            //    {
            //        button.Enabled = false;
            //        fileupload.Enabled = false;

            //    }

            //}

        }

        protected void Time_Tick(object sender, EventArgs e)
        {
            
            //var t = DateTime.Now;
            //lblup.Text = t.ToString();
            //bindgrid();
            
        }
       
    }
}