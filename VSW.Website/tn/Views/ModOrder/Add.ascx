<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModOrderModel;
    var item = ViewBag.Data as ModOrderEntity;

    var listItem = item.GetOrderDetail();

    string html = !string.IsNullOrEmpty(item.Name) ? item.Name + "\n": "";
    html += !string.IsNullOrEmpty(item.Email) ? item.Email + "\n": "";
    html += !string.IsNullOrEmpty(item.Phone) ? item.Phone + "\n": "";
    html += !string.IsNullOrEmpty(item.Address) ? item.Address + "\n": "";
    html += !string.IsNullOrEmpty(item.Content) ? item.Content + "\n\n" : "";
%>

<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />

    <div class="page-content-wrapper">
        <h3 class="page-title">Đơn hàng <small><%= model.RecordID > 0 ? "Chỉnh sửa": "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Đơn hàng</a>
                </li>
            </ul>
            <div class="page-toolbar">
                <div class="btn-group">
                    <a href="/" class="btn green" target="_blank"><i class="icon-screen-desktop"></i>Xem Website</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <%= ShowMessage()%>

                <div class="form-horizontal form-row-seperated">
                    <div class="portlet">
                        <div class="portlet-title">
                            <div class="caption"></div>
                            <div class="actions btn-set">
                                <%= GetSortAddCommand()%>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Khách hàng</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Họ và tên:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Name" value="<%=item.Name %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Điện thoại:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Phone" value="<%=item.Phone %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Email:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Email" value="<%=item.Email %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tỉnh/Thành phố:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" value="<%=GetName(item.GetWard()) + " - " + GetName(item.GetDistrict()) + " - " + GetName(item.GetCity())%>" readonly="readonly" />
                                                    </div>
                                                </div>
                                              <%-- <%if(string.IsNullOrEmpty(item.Address)){%>--%>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Địa chỉ:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" value="<%=item.Address %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                         
<%--                                            <%if (item.Formality && item.Showroom > 0){%>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Showroom xem hàng:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" value="<%=item.GetShowroom().Name %> (<%=item.GetShowroom().Address %>)" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <%} %>--%>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Hình thức thanh toán:</label>
                                                    <div class="col-md-9">
                                                   <input type="text" class="form-control" value="<%=item.Payment%>" readonly="readonly" />
                                                        </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Ghi chú:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control" rows="5" name="Content" readonly="readonly"><%=item.Content%></textarea>
                                                    </div>
                                                </div>


                                                
                                                <%if (item.Invoice)
                                                    { %>
                                                <div class="form-group row">
                                                    <label class="col-md-3 text-right"></label>
                                                    <div class="col-md-9">
                                                        <b>Thông tin xuất hóa đơn</b>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 text-right">Tên công ty:</label>
                                                    <div class="col-md-9">
                                                        <b><%=item.CompanyName %></b>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 text-right">Địa chỉ công ty:</label>
                                                    <div class="col-md-9">
                                                        <b><%=item.CompanyAddress %></b>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 text-right">Mã số thuế công ty:</label>
                                                    <div class="col-md-9">
                                                        <b><%=item.CompanyTax %></b>
                                                    </div>
                                                </div>
                                                <%} %>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Đơn hàng</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Trạng thái:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="StatusID" id="StatusID">
                                                            <option value="0">Root</option>
                                                            <%= Utils.ShowDdlMenuByType2("Status", model.LangID, item.StatusID)%>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mã đơn hàng:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Code" value="<%=item.Code %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tổng tiền:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Total" value="<%= string.Format("{0:#,##0}", item.Total)%>" readonly="readonly" />
                                                        <span class="help-block text-primary"><%=Utils.NumberToWord(item.Total.ToString()) %></span>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">IP:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="IP" value="<%=item.IP %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Ngày mua:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Address" value="<%=string.Format("{0:HH:mm - dd/MM/yyyy}", item.Created) %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Sản phẩm</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="dataTables_wrapper">
                                                <div class="table-scrollable">
                                                    <table class="table table-striped table-hover table-bordered dataTable">
                                                        <thead>
                                                            <tr>
                                                                <th class="sorting text-center w1p">#</th>
                                                                <th class="sorting"><%= GetSortUnLink("Tên sản phẩm", "Name")%></th>
                                                                <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortUnLink("Ảnh", "File")%></th>
                                                                <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortUnLink("Giá bán", "Price")%></th>
                                                                <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortUnLink("Số lượng", "Quantity")%></th>
                                                                <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortUnLink("Thành tiền", "Total")%></th>
                                                                <th class="sorting text-center w1p">#</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%for (var i = 0; listItem != null && i < listItem.Count; i++){
                                                                    var product = ModProductService.Instance.GetByID(listItem[i].ProductID);
                                                                    if (product == null) continue;

                                                                    html += (i + 1) + ") " + product.Name + " - Số lượng: " + listItem[i].Quantity + "\n";
                                                            %>
                                                            <tr>
                                                                <td class="text-center"><%= i + 1%></td>
                                                                <td>
                                                                    <a href="/{CPPath}/ModProduct/Add.aspx/RecordID/<%= product.ID %>" target="_blank"><%= product.Name%></a>
                                                                    <p class="smallsub hidden-sm hidden-col">(<span>Mã</span>: <%= product.Code%>)</p>
                                                                </td>
                                                                <td class="text-center hidden-sm hidden-col">
                                                                    <%= Utils.GetMedia(product.File, 40, 40)%>
                                                                </td>
                                                                <td class="text-center hidden-sm hidden-col"><%= string.Format("{0:#,##0}", product.Price) %></td>
                                                                <td class="text-center hidden-sm hidden-col"><%= string.Format("{0:#,##0}", listItem[i].Quantity) %></td>
                                                                <td class="text-center hidden-sm hidden-col"><%= string.Format("{0:#,##0}", product.Price * listItem[i].Quantity) %></td>
                                                                <td class="text-center hidden-sm hidden-col"><%= listItem[i].ID%></td>
                                                            </tr>
                                                            <%} %>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 justify-content-center d-flex">
                                                        <span class="help-block text-primary">Tổng tiền: <%=string.Format("{0:#,##0}", item.Total) %> VNĐ</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Tiện ích</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Nội dung:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control" rows="10" id="html"><%=html%></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">&nbsp;</label>
                                                    <div class="col-md-9">
                                                        <button type="button" class="btn btn-primary" onclick="copyToClipboard('#html'); return false">Copy</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</form>