using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;

namespace PEJ.ClasesAddenda
{
    public class Interfactura
    {
        /// <summary>
        /// Metodos que obtiene el encabezado del documento[Factura/Noda de crédito/Nota de débito]
        /// </summary>
        /// <param name="Opcion">@TipoConsulta Store Procedure PEF_Fectura</param>
        /// <returns>DataRow</returns>
        public static DataRow GetEncabezado(int Opcion, int Folio, string tipoDocto)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PEJ_Facturacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", Opcion);
                    command.Parameters.AddWithValue("@Factura", Folio);
                    command.Parameters.AddWithValue("@TipoDocumento", tipoDocto);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    if (table.Rows.Count > 0) return table.Rows[0];
                    else return new DataTable().NewRow();
                }
            }

        }


        /// <summary>
        /// Metodos que obtiene el detalle del documento[Factura/Noda de crédito/Nota de débito]
        /// </summary>
        /// <param name="Opcion">@TipoConsulta Store Procedure PEF_Fectura</param>
        /// <returns>DataRow</returns>
        public static DataTable GetDetalle(int Opcion, int Folio, string tipoDocto)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PEJ_Facturacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", Opcion);
                    command.Parameters.AddWithValue("@Factura", Folio);
                    command.Parameters.AddWithValue("@TipoDocumento", tipoDocto);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    return table;
                }
            }
        }

        /// <summary>
        /// Metodos que agruega un nuevo nodo
        /// </summary>
        /// <param name="document"></param>
        /// /// <param name="document"></param>
        /// /// <param name="parent"></param>
        /// /// <param name="name"></param>
        /// /// <param name="value"></param>
        /// /// <param name="nameSpace">Indica si el nodo pertenece a un Espacio de nombres</param>
        /// <returns>DataRow</returns>
        private static XmlNode AddNode(XmlDocument document, XmlNode parent, string name, string value, bool nameSpace)
        {
            if (nameSpace)
            {
                XmlNode child = document.CreateNode(XmlNodeType.Element, name, "https://www.interfactura.com/Schemas/Documentos");
                if (value != null)
                    child.InnerText = value;
                parent.AppendChild(child);

                return child;
            }
            else
            {
                XmlNode child = document.CreateNode(XmlNodeType.Element, name, parent.NamespaceURI);
                if (value != null)
                    child.InnerText = value;
                parent.AppendChild(child);

                return child;
                //XmlNode child = document.CreateElement(name);
                //if (value != null)
                //    child.InnerText = value;
                //parent.AppendChild(child);

                //return child;
            }

        }

        /// <summary>
        /// Metodos que agruega un nuevo nodo
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="nameSpace">Indica si el nodo pertenece a un Espacio de nombres</param>
        /// <returns>DataRow</returns>
        private static XmlNode AddNode(XmlDocument document, XmlNode parent, string name, bool nameSpace)
        {
            return AddNode(document, parent, name, null, nameSpace);
        }

        /// <summary>
        /// Metodos que agruega un nuevo atributo a un nodo
        /// </summary>
        /// <param name="document"></param>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>DataRow</returns>
        private static XmlAttribute AddAttribute(XmlDocument document, XmlNode node, string name, string value)
        {
            XmlAttribute attr = document.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
            return attr;
        }


        public void AddedaFactura(int Folio, string tipoDocto)
        {
            try
            {
                string _Path = "";
                DataRow data1 = GetEncabezado(9, Folio, tipoDocto);

                if (data1.Table.Columns.Count > 0)
                {
                    if ((data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year != DateTime.Now.Year)
                                || (data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year))
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<DateTime>("Fecha").Month + "-" + data1.Field<DateTime>("Fecha").Year + "\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    else if (data1.Field<DateTime>("Fecha").Month == DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year)
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    if (!string.IsNullOrEmpty(_Path))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(_Path);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                        nsmgr.AddNamespace("if", "https://www.interfactura.com/Schemas/Documentos");

                        XmlNodeList comprobante = xDoc.GetElementsByTagName("cfdi:Comprobante");
                        XmlNode parent = comprobante.Item(0);

                        //Nodo Raiz Addenda
                        XmlNode nodoAddenda = AddNode(xDoc, parent, "cfdi:Addenda", string.Empty, false);

                        //nodo INTERFACTURA
                        XmlNode nodoInterFactura = AddNode(xDoc, nodoAddenda, "if:FacturaInterfactura", string.Empty, true);
                        AddAttribute(xDoc, nodoInterFactura, "TipoDocumento", "Factura");

                        //Nodo Emisor
                        XmlNode nodoEmisor = AddNode(xDoc, nodoInterFactura, "if:Emisor", string.Empty, true);
                        AddAttribute(xDoc, nodoEmisor, "RI", "0140810");

                        //Nodo Receptor
                        XmlNode nodoReceptor = AddNode(xDoc, nodoInterFactura, "if:Receptor", string.Empty, true);
                        AddAttribute(xDoc, nodoReceptor, "RI", "00009");

                        //Nodo Encabezado
                        XmlNode nodoEncabezado = AddNode(xDoc, nodoInterFactura, "if:Encabezado", string.Empty, true);
                        AddAttribute(xDoc, nodoEncabezado, "TipoProveedorEKT", "1");
                        AddAttribute(xDoc, nodoEncabezado, "Fecha", data1.Field<DateTime>("Fecha").ToString("yyy-MM-ddThh:mm:ss K"));
                        AddAttribute(xDoc, nodoEncabezado, "MonedaDoc", data1.Field<string>("MonedaDoc"));
                        AddAttribute(xDoc, nodoEncabezado, "IVAPCT", "16");
                        AddAttribute(xDoc, nodoEncabezado, "Iva", data1.Field<decimal>("Iva").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "SubTotal", data1.Field<decimal>("SubTotal").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "Total", data1.Field<decimal>("Total").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "NumProveedor", data1.Field<string>("NumProveedor"));
                        AddAttribute(xDoc, nodoEncabezado, "FolioOrdenCompra", data1.Field<int>("FolioOrdenCompra").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuento", "0.0");
                        AddAttribute(xDoc, nodoEncabezado, "PorcentajeMerma", "0");
                        AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuentoPromo", "0.0");
                        AddAttribute(xDoc, nodoEncabezado, "TotalDescuento", "0");
                        AddAttribute(xDoc, nodoEncabezado, "TotalMerma", "0");
                        AddAttribute(xDoc, nodoEncabezado, "Descuento", "0.0");
                        AddAttribute(xDoc, nodoEncabezado, "Observaciones", string.Empty);

                        DataTable items = GetDetalle(10, Folio, tipoDocto);
                        foreach (DataRow item in items.Rows)
                        {
                            XmlNode nodoCuerpo = AddNode(xDoc, nodoEncabezado, "if:Cuerpo", string.Empty, true);
                            AddAttribute(xDoc, nodoCuerpo, "Renglon", item.Field<int>("Renglon").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Cantidad", item.Field<decimal>("Cantidad").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Concepto", item.Field<string>("Concepto").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "PUnitario", item.Field<decimal>("PUnitario").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Importe", item.Field<decimal>("Importe").ToString());
                        }

                        xDoc.Save("\\\\192.168.2.100\\xml\\Elektra\\Facturas\\" + data1.Field<string>("EDocNum") + ".xml");
                    }
                }
            }catch(Exception )
            {
            }
        }
        public void AddedaDebito(int Folio, string tipoDocto)
        {
            try
            {
                string _Path = "";
                DataRow data1 = GetEncabezado(9, Folio, tipoDocto);

                if (data1.Table.Columns.Count > 0)
                {
                    if ((data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year != DateTime.Now.Year)
                                || (data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year))
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<DateTime>("Fecha").Month + "-" + data1.Field<DateTime>("Fecha").Year + "\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    else if (data1.Field<DateTime>("Fecha").Month == DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year)
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    if (!string.IsNullOrEmpty(_Path))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(_Path);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                        nsmgr.AddNamespace("if", "https://www.interfactura.com/Schemas/Documentos");

                        XmlNodeList comprobante = xDoc.GetElementsByTagName("cfdi:Comprobante");
                        XmlNode parent = comprobante.Item(0);

                        //Nodo Raiz Addenda
                        XmlNode nodoAddenda = AddNode(xDoc, parent, "cfdi:Addenda", string.Empty, false);

                        //nodo INTERFACTURA
                        XmlNode nodoInterFactura = AddNode(xDoc, nodoAddenda, "if:FacturaInterfactura", string.Empty, true);
                        // AddAttribute(xDoc, nodoInterFactura, "xmlns:if", "https://www.interfactura.com/Schemas/Documentos");
                        AddAttribute(xDoc, nodoInterFactura, "TipoDocumento", "NotaDebito");

                        //Nodo Emisor
                        XmlNode nodoEmisor = AddNode(xDoc, nodoInterFactura, "if:Emisor", string.Empty, true);
                        AddAttribute(xDoc, nodoEmisor, "RI", "0140810");

                        //Nodo Receptor
                        XmlNode nodoReceptor = AddNode(xDoc, nodoInterFactura, "if:Receptor", string.Empty, true);
                        AddAttribute(xDoc, nodoReceptor, "RI", "00009");

                        //Nodo Encabezado
                        XmlNode nodoEncabezado = AddNode(xDoc, nodoInterFactura, "if:Encabezado", string.Empty, true);
                        AddAttribute(xDoc, nodoEncabezado, "TipoProveedorEKT", "1");
                        AddAttribute(xDoc, nodoEncabezado, "Fecha", data1.Field<DateTime>("Fecha").ToString("yyy-MM-ddThh:mm:ss K"));
                        AddAttribute(xDoc, nodoEncabezado, "MonedaDoc", data1.Field<string>("MonedaDoc"));
                        AddAttribute(xDoc, nodoEncabezado, "IVAPCT", "16");
                        AddAttribute(xDoc, nodoEncabezado, "Iva", data1.Field<decimal>("Iva").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "SubTotal", data1.Field<decimal>("SubTotal").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "Total", data1.Field<decimal>("Total").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "NumProveedor", data1.Field<string>("NumProveedor"));
                        //AddAttribute(xDoc, nodoEncabezado, "FolioOrdenCompra", data1.Field<int>("FolioOrdenCompra").ToString());
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuento", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeMerma", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuentoPromo", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "TotalDescuento", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "TotalMerma", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "Descuento", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "Observaciones", string.Empty);

                        DataTable items = GetDetalle(10, Folio, tipoDocto);
                        foreach (DataRow item in items.Rows)
                        {
                            XmlNode nodoCuerpo = AddNode(xDoc, nodoEncabezado, "if:Cuerpo", string.Empty, true);
                            AddAttribute(xDoc, nodoCuerpo, "Renglon", item.Field<int>("Renglon").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Cantidad", item.Field<decimal>("Cantidad").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Concepto", item.Field<string>("Concepto").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "PUnitario", item.Field<decimal>("PUnitario").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Importe", item.Field<decimal>("Importe").ToString());
                        }

                        xDoc.Save("\\\\192.168.2.100\\xml\\Elektra\\Debito\\" + data1.Field<string>("EDocNum") + ".xml");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void AddedaCredito(int Folio, string tipoDocto)
        {
            try
            {
                string _Path = "";
                DataRow data1 = GetEncabezado(9, Folio, tipoDocto);
                if (data1.Table.Columns.Count > 0)
                {
                    if ((data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year != DateTime.Now.Year)
                                || (data1.Field<DateTime>("Fecha").Month != DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year))
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<DateTime>("Fecha").Month + "-" + data1.Field<DateTime>("Fecha").Year + "\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    else if (data1.Field<DateTime>("Fecha").Month == DateTime.Now.Month && data1.Field<DateTime>("Fecha").Year == DateTime.Now.Year)
                    {
                        _Path = "\\\\192.168.2.100\\xml\\" + data1.Field<string>("EDocNum") + ".xml";
                    }
                    if (!string.IsNullOrEmpty(_Path))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(_Path);

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                        nsmgr.AddNamespace("if", "https://www.interfactura.com/Schemas/Documentos");

                        XmlNodeList comprobante = xDoc.GetElementsByTagName("cfdi:Comprobante");
                        XmlNode parent = comprobante.Item(0);

                        //Nodo Raiz Addenda
                        XmlNode nodoAddenda = AddNode(xDoc, parent, "cfdi:Addenda", string.Empty, false);

                        //nodo INTERFACTURA
                        XmlNode nodoInterFactura = AddNode(xDoc, nodoAddenda, "if:FacturaInterfactura", string.Empty, true);
                        AddAttribute(xDoc, nodoInterFactura, "TipoDocumento", "NotaCredito");

                        //Nodo Emisor
                        XmlNode nodoEmisor = AddNode(xDoc, nodoInterFactura, "if:Emisor", string.Empty, true);
                        AddAttribute(xDoc, nodoEmisor, "RI", "0140810");

                        //Nodo Receptor
                        XmlNode nodoReceptor = AddNode(xDoc, nodoInterFactura, "if:Receptor", string.Empty, true);
                        AddAttribute(xDoc, nodoReceptor, "RI", "00009");

                        //Nodo Encabezado
                        XmlNode nodoEncabezado = AddNode(xDoc, nodoInterFactura, "if:Encabezado", string.Empty, true);
                        AddAttribute(xDoc, nodoEncabezado, "TipoProveedorEKT", "1");
                        AddAttribute(xDoc, nodoEncabezado, "Fecha", data1.Field<DateTime>("Fecha").ToString("yyy-MM-ddThh:mm:ss K"));
                        AddAttribute(xDoc, nodoEncabezado, "MonedaDoc", data1.Field<string>("MonedaDoc"));
                        AddAttribute(xDoc, nodoEncabezado, "IVAPCT", "16");
                        AddAttribute(xDoc, nodoEncabezado, "Iva", data1.Field<decimal>("Iva").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "SubTotal", data1.Field<decimal>("SubTotal").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "Total", data1.Field<decimal>("Total").ToString());
                        AddAttribute(xDoc, nodoEncabezado, "NumProveedor", data1.Field<string>("NumProveedor"));
                        //AddAttribute(xDoc, nodoEncabezado, "FolioOrdenCompra", data1.Field<int>("FolioOrdenCompra").ToString());
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuento", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeMerma", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "PorcentajeDescuentoPromo", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "TotalDescuento", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "TotalMerma", "0");
                        //AddAttribute(xDoc, nodoEncabezado, "Descuento", "0.0");
                        //AddAttribute(xDoc, nodoEncabezado, "Observaciones", string.Empty);

                        DataTable items = GetDetalle(10, Folio, tipoDocto);
                        foreach (DataRow item in items.Rows)
                        {
                            XmlNode nodoCuerpo = AddNode(xDoc, nodoEncabezado, "if:Cuerpo", string.Empty, true);
                            AddAttribute(xDoc, nodoCuerpo, "Renglon", item.Field<int>("Renglon").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Cantidad", item.Field<decimal>("Cantidad").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Concepto", item.Field<string>("Concepto").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "PUnitario", item.Field<decimal>("PUnitario").ToString());
                            AddAttribute(xDoc, nodoCuerpo, "Importe", item.Field<decimal>("Importe").ToString());
                        }

                        xDoc.Save("\\\\192.168.2.100\\xml\\Elektra\\Credito\\" + data1.Field<string>("EDocNum") + ".xml");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
