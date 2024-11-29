<%@ Page Language="C#" AutoEventWireup="true" %>
<script runat="server">
    private int GetMaxOrder()
    {
        return WebMenuService.Instance.CreateQuery()
                        .Max(o => o.Order)
                        .ToValue().ToInt(0) + 1;
    }

    int Insert(int parent_id, string name)
    {
        int id = WebMenuService.Instance.CreateQuery()
                        .Where(o => o.Name == name && o.ParentID == parent_id)
                        .ToValue().ToInt(0);

        if (id > 0) return id;

        WebMenuEntity entity = new WebMenuEntity();
        entity.LangID = 1;
        entity.Type = "City";
        entity.ParentID = parent_id;
        entity.Name = name;
        entity.Code = Data.GetCode(entity.Name);
        entity.Order = GetMaxOrder();
        entity.Activity = true;

        return WebMenuService.Instance.Save(entity);
    }

    void Insert(int parent_id)
    {
        System.Data.DataSet ds = new System.Data.DataSet();
        ds.ReadXml(Server.MapPath("City.xml"));
        System.Data.DataTable dtCity = ds.Tables[0];

        for (int i = 0; i < dtCity.Rows.Count; i++)
        {
            int city_id = Insert(parent_id, dtCity.Rows[i]["city"].ToString());
            int district_id = Insert(city_id, dtCity.Rows[i]["district"].ToString());
            //Insert(district_id, dtCity.Rows[i]["ward"].ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Insert(1668);
        Response.Write("OK");
    }
</script>