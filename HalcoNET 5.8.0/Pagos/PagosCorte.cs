using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pagos
{
    public partial class PagosCorte : Form
    {
        private DateTime Inicio;
        private DateTime Corte1;
        private DateTime Corte2;
        private DateTime Corte3;
        private DateTime Corte4;
        private DateTime Corte5;
        decimal TC = decimal.Zero;

        private enum Columnas
        {
            Tipo,
            Nombre,
            Corte1_MXP_A,
            Corte1_USD_A,
            Corte1_MXP_B,
            Corte1_USD_B,

            Corte2_MXP_A,
            Corte2_USD_A,
            Corte2_MXP_B,
            Corte2_USD_B,

            Corte3_MXP_A,
            Corte3_USD_A,
            Corte3_MXP_B,
            Corte3_USD_B,

            Corte4_MXP_A,
            Corte4_USD_A,
            Corte4_MXP_B,
            Corte4_USD_B,

            Corte5_MXP_A,
            Corte5_USD_A,
            Corte5_MXP_B,
            Corte5_USD_B,

            GroupName
        }

        private enum ColumnasTotal
        {
            Descripcion,
            Corte1,
            Corte1USD,
            Corte2,
            Corte2USD,
            Corte3,
            Corte3USD,
            Corte4,
            Corte4USD,
            Corte5,
            Corte5USD
        }

        private string qry = @"declare @Proveedor nvarchar(500)

                        declare @PAGOS TABLE(TipoColumna varchar(2), Proveedor NVARCHAR(500), 
						                        corte1MXP_A decimal(18,4), corte1MXP_B decimal(18,4), corte1USD_A decimal(18,4), corte1USD_B decimal(18,4),
						                        corte2MXP_A decimal(18,4), corte2MXP_B decimal(18,4), corte2USD_A decimal(18,4), corte2USD_B decimal(18,4),
						                        corte3MXP_A decimal(18,4), corte3MXP_B decimal(18,4), corte3USD_A decimal(18,4), corte3USD_B decimal(18,4),
						                        corte4MXP_A decimal(18,4), corte4MXP_B decimal(18,4), corte4USD_A decimal(18,4), corte4USD_B decimal(18,4),
						                        corte5MXP_A decimal(18,4), corte5MXP_B decimal(18,4), corte5USD_A decimal(18,4), corte5USD_B decimal(18,4))


                        SELECT 
	                        Provedor 'Proveedor'
	                        , [Fecha de vencimiento] 
	                        , Nombre
	                        , Clasificacion
	                        , SUM(CASE WHEN DocCur = '$' THEN Total END) MXP
	                        , SUM(CASE WHEN DocCur = 'USD' THEN Total END) USD
                            , GroupName
                        INTO #DATOS
                        FROM(
	                        SELECT 
                                E.GroupName,
		                        'FR' Tipo,
		                        CASE WHEN DocDueDate >= GETDATE() Then 'Por Vencer'
			                        ELSE CASE WHEN DocDueDate <= GETDATE() THEN 'Vencido'
			                        END END as 'Estatus',
		                        A.CardCode 'Provedor',
		                        A.CardName 'Nombre',
		                        A.DocDate 'Fecha de Contabilización',
		                        A.DocDueDate 'Fecha de vencimiento',
		                        A.NumAtCard 'Folio Factura',
		                        A.DocNum 'Folio SAP',
		                        A.DocCur
		                        ,CASE WHEN A.DocCur = 'USD' THEN (A.DocTotalFC - A.PaidFC) ELSE (A.DocTotal-A.PaidToDate) END AS 'Total'
		                        ,A.U_FP
		                        ,A.DocEntry
		                        ,ISNULL(B.BankCode,'') BankCode,ISNULL(F.BankName,'') BankName,ISNULL(B.DflAccount,'') Cuenta 
		                        ,CAST(B.U_Condiciones as nvarchar(MAX)) U_Condiciones
                                ,ISNULL((SELECT Top 1 Clasificacion FROM [SGUV].[dbo].[ProvsClasif] PC WHERE PC.CardCode COLLATE DATABASE_DEFAULT = B.CardCode), 'B') Clasificacion
	                        FROM [SBO-DistPJ].[dbo].OPCH A
		                        INNER JOIN [SBO-DistPJ].[dbo].OCRD B ON A.CardCode = B.CardCode AND B.CardCode NOT IN ('20012','21478')
		                        INNER JOIN [SBO-DistPJ].[dbo].OCTG D ON B.GroupNum = D.GroupNum
		                        INNER JOIN [SBO-DistPJ].[dbo].OCRG E ON B.GroupCode = E.GroupCode 
		                        LEFT JOIN [SBO-DistPJ].[dbo].ODSC F ON B.BankCode = F.BankCode			
	                        WHERE A.DocStatus = 'O' AND B.GroupCode IN (110,111,112,113,118))tmp
                        WHERE Convert(Date, [Fecha de vencimiento]) BETWEEN Convert(Date, @INICIO) AND Convert(Date, @CORTE5)
                              AND (GroupName != 'Bancos' AND Provedor not in  ('20248', '20250', '20705')
		                            OR GroupName = 'Bancos' AND Provedor in  ('20248', '20250', '20705'))
                        GROUP BY Provedor, Nombre, DocCur, [Fecha de vencimiento], Clasificacion, GroupName
                        ORDER BY Nombre


                        SELECT 
	                        1 Tipo,
	                        Nombre,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] >= Convert(Date, @INICIO) AND [Fecha de vencimiento] <= Convert(Date, @CORTE1) AND Clasificacion = 'A'
		                        THEN MXP END), 0) CORTE1_MXP_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] >= Convert(Date, @INICIO) AND [Fecha de vencimiento] <= Convert(Date, @CORTE1) AND Clasificacion = 'B'
		                        THEN MXP END), 0) CORTE1_MXP_B,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] >= Convert(Date, @INICIO) AND [Fecha de vencimiento] <= Convert(Date, @CORTE1) AND Clasificacion = 'A'
		                        THEN USD END), 0) CORTE1_USD_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] >= Convert(Date, @INICIO) AND [Fecha de vencimiento] <= Convert(Date, @CORTE1) AND Clasificacion = 'B'
		                        THEN USD END), 0) CORTE1_USD_B,
		
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE1) AND [Fecha de vencimiento] <= Convert(Date, @CORTE2) AND Clasificacion = 'A'
		                        THEN MXP END), 0) CORTE2_MXP_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE1) AND [Fecha de vencimiento] <= Convert(Date, @CORTE2) AND Clasificacion = 'B'
		                        THEN MXP END), 0) CORTE2_MXP_B,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE1) AND [Fecha de vencimiento] <= Convert(Date, @CORTE2) AND Clasificacion = 'A'
		                        THEN USD END), 0) CORTE2_USD_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE1) AND [Fecha de vencimiento] <= Convert(Date, @CORTE2) AND Clasificacion = 'B'
		                        THEN USD END), 0) CORTE2_USD_B,
		
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE2) AND [Fecha de vencimiento] <= Convert(Date, @CORTE3) AND Clasificacion = 'A'
		                        THEN MXP END), 0) CORTE3_MXP_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE2) AND [Fecha de vencimiento] <= Convert(Date, @CORTE3) AND Clasificacion = 'B'
		                        THEN MXP END), 0) CORTE3_MXP_B,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE2) AND [Fecha de vencimiento] <= Convert(Date, @CORTE3) AND Clasificacion = 'A'
		                        THEN USD END), 0) CORTE3_USD_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE2) AND [Fecha de vencimiento] <= Convert(Date, @CORTE3) AND Clasificacion = 'B'
		                        THEN USD END), 0) CORTE3_USD_B,
	
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE3) AND [Fecha de vencimiento] <= Convert(Date, @CORTE4) AND Clasificacion = 'A'
		                        THEN MXP END), 0) CORTE4_MXP_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE3) AND [Fecha de vencimiento] <= Convert(Date, @CORTE4) AND Clasificacion = 'B'
		                        THEN MXP END), 0) CORTE4_MXP_B,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE3) AND [Fecha de vencimiento] <= Convert(Date, @CORTE4) AND Clasificacion = 'A'
		                        THEN USD END), 0) CORTE4_USD_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE3) AND [Fecha de vencimiento] <= Convert(Date, @CORTE4) AND Clasificacion = 'B'
		                        THEN USD END), 0) CORTE4_USD_B,
		
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE4) AND [Fecha de vencimiento] <= Convert(Date, @CORTE5) AND Clasificacion = 'A'
		                        THEN MXP END), 0) CORTE5_MXP_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE4) AND [Fecha de vencimiento] <= Convert(Date, @CORTE5) AND Clasificacion = 'B'
		                        THEN MXP END), 0) CORTE5_MXP_B,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE4) AND [Fecha de vencimiento] <= Convert(Date, @CORTE5) AND Clasificacion = 'A'
		                        THEN USD END), 0) CORTE5_USD_A,
	                        ISNULL(SUM(CASE WHEN [Fecha de vencimiento] > Convert(Date, @CORTE4) AND [Fecha de vencimiento] <= Convert(Date, @CORTE5) AND Clasificacion = 'B'
		                        THEN USD END), 0) CORTE5_USD_B	

                            , GroupName
                        FROM 
	                        #DATOS
                        GROUP BY Nombre, GroupName
                        ORDER BY GroupName, Nombre

                        DROP TABLE #DATOS";

        public void Formato(DataGridView dgv)
        {
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dgvHeader.Columns[0].Visible = false;
            dgv.Columns[(int)Columnas.Nombre].HeaderText = string.Empty;
            dgv.Columns[(int)Columnas.Tipo].Visible = false;
            dgv.Columns[(int)Columnas.GroupName].Visible = false;

            dgvHeader.Columns[1].Width = dgv.Columns[(int)Columnas.Nombre].Width;
            dgvHeader.Columns[2].Width = dgv.Columns[(int)Columnas.Corte1_MXP_A].Width + dgv.Columns[(int)Columnas.Corte1_MXP_B].Width + dgv.Columns[(int)Columnas.Corte1_USD_A].Width + dgv.Columns[(int)Columnas.Corte1_USD_B].Width;
            dgvHeader.Columns[3].Width = dgv.Columns[(int)Columnas.Corte2_MXP_A].Width + dgv.Columns[(int)Columnas.Corte2_MXP_B].Width + dgv.Columns[(int)Columnas.Corte2_USD_A].Width + dgv.Columns[(int)Columnas.Corte2_USD_B].Width;
            dgvHeader.Columns[4].Width = dgv.Columns[(int)Columnas.Corte3_MXP_A].Width + dgv.Columns[(int)Columnas.Corte3_MXP_B].Width + dgv.Columns[(int)Columnas.Corte3_USD_A].Width + dgv.Columns[(int)Columnas.Corte3_USD_B].Width;
            dgvHeader.Columns[5].Width = dgv.Columns[(int)Columnas.Corte4_MXP_A].Width + dgv.Columns[(int)Columnas.Corte4_MXP_B].Width + dgv.Columns[(int)Columnas.Corte4_USD_A].Width + dgv.Columns[(int)Columnas.Corte4_USD_B].Width;
            dgvHeader.Columns[6].Width = dgv.Columns[(int)Columnas.Corte5_MXP_A].Width + dgv.Columns[(int)Columnas.Corte5_MXP_B].Width + dgv.Columns[(int)Columnas.Corte5_USD_A].Width + dgv.Columns[(int)Columnas.Corte5_USD_B].Width + 13;

            dgvHeader.Columns[2].DividerWidth = 2;
            dgvHeader.Columns[2].DividerWidth = 2;
            dgvHeader.Columns[2].DividerWidth = 2;
            dgvHeader.Columns[2].DividerWidth = 2;
            dgvHeader.Columns[2].DividerWidth = 2;

            if (!check2.Checked)
            {
                dgv.Columns[(int)Columnas.Corte1_MXP_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte1_MXP_B].Visible = false;
                dgv.Columns[(int)Columnas.Corte1_USD_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte1_USD_B].Visible = false;
                dgvHeader.Columns[2].Visible = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Corte1_MXP_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte1_MXP_B].Visible = true;
                dgv.Columns[(int)Columnas.Corte1_USD_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte1_USD_B].Visible = true;
                dgvHeader.Columns[2].Visible = true;
            }
            dgv.Columns[(int)Columnas.Corte1_MXP_A].HeaderText = "MXP+";
            dgv.Columns[(int)Columnas.Corte1_MXP_B].HeaderText = "MXP-";
            dgv.Columns[(int)Columnas.Corte1_USD_A].HeaderText = "USD+";
            dgv.Columns[(int)Columnas.Corte1_USD_B].HeaderText = "USD-";
            dgv.Columns[(int)Columnas.Corte1_MXP_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte1_MXP_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte1_USD_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte1_USD_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte1_MXP_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte1_MXP_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte1_USD_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte1_USD_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte1_USD_B].DividerWidth = 2;
            if (!check3.Checked)
            {
                dgv.Columns[(int)Columnas.Corte2_MXP_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte2_MXP_B].Visible = false;
                dgv.Columns[(int)Columnas.Corte2_USD_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte2_USD_B].Visible = false;
                dgvHeader.Columns[3].Visible = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Corte2_MXP_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte2_MXP_B].Visible = true;
                dgv.Columns[(int)Columnas.Corte2_USD_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte2_USD_B].Visible = true;
                dgvHeader.Columns[3].Visible = true;
            }
            dgv.Columns[(int)Columnas.Corte2_MXP_A].HeaderText = "MXP+";
            dgv.Columns[(int)Columnas.Corte2_MXP_B].HeaderText = "MXP-";
            dgv.Columns[(int)Columnas.Corte2_USD_A].HeaderText = "USD+";
            dgv.Columns[(int)Columnas.Corte2_USD_B].HeaderText = "USD-";
            dgv.Columns[(int)Columnas.Corte2_MXP_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte2_MXP_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte2_USD_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte2_USD_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte2_MXP_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte2_MXP_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte2_USD_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte2_USD_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte2_USD_B].DividerWidth = 2;

            if (!check4.Checked)
            {
                dgv.Columns[(int)Columnas.Corte3_MXP_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte3_MXP_B].Visible = false;
                dgv.Columns[(int)Columnas.Corte3_USD_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte3_USD_B].Visible = false;
                dgvHeader.Columns[4].Visible = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Corte3_MXP_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte3_MXP_B].Visible = true;
                dgv.Columns[(int)Columnas.Corte3_USD_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte3_USD_B].Visible = true;
                dgvHeader.Columns[4].Visible = true;
            }
            dgv.Columns[(int)Columnas.Corte3_MXP_A].HeaderText = "MXP+";
            dgv.Columns[(int)Columnas.Corte3_MXP_B].HeaderText = "MXP-";
            dgv.Columns[(int)Columnas.Corte3_USD_A].HeaderText = "USD+";
            dgv.Columns[(int)Columnas.Corte3_USD_B].HeaderText = "USD-";
            dgv.Columns[(int)Columnas.Corte3_MXP_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte3_MXP_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte3_USD_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte3_USD_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte3_MXP_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte3_MXP_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte3_USD_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte3_USD_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte3_USD_B].DividerWidth = 2;

            if (!check5.Checked)
            {
                dgv.Columns[(int)Columnas.Corte4_MXP_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte4_MXP_B].Visible = false;
                dgv.Columns[(int)Columnas.Corte4_USD_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte4_USD_B].Visible = false;
                dgvHeader.Columns[5].Visible = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Corte4_MXP_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte4_MXP_B].Visible = true;
                dgv.Columns[(int)Columnas.Corte4_USD_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte4_USD_B].Visible = true;
                dgvHeader.Columns[5].Visible = true;
            }
            dgv.Columns[(int)Columnas.Corte4_MXP_A].HeaderText = "MXP+";
            dgv.Columns[(int)Columnas.Corte4_MXP_B].HeaderText = "MXP-";
            dgv.Columns[(int)Columnas.Corte4_USD_A].HeaderText = "USD+";
            dgv.Columns[(int)Columnas.Corte4_USD_B].HeaderText = "USD-";
            dgv.Columns[(int)Columnas.Corte4_MXP_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte4_MXP_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte4_USD_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte4_USD_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte4_MXP_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte4_MXP_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte4_USD_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte4_USD_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte4_USD_B].DividerWidth = 2;

            if (!check6.Checked)
            {
                dgv.Columns[(int)Columnas.Corte5_MXP_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte5_MXP_B].Visible = false;
                dgv.Columns[(int)Columnas.Corte5_USD_A].Visible = false;
                dgv.Columns[(int)Columnas.Corte5_USD_B].Visible = false;
                dgvHeader.Columns[6].Visible = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Corte5_MXP_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte5_MXP_B].Visible = true;
                dgv.Columns[(int)Columnas.Corte5_USD_A].Visible = true;
                dgv.Columns[(int)Columnas.Corte5_USD_B].Visible = true;
                dgvHeader.Columns[6].Visible = true;
            }
            dgv.Columns[(int)Columnas.Corte5_MXP_A].HeaderText = "MXP+";
            dgv.Columns[(int)Columnas.Corte5_MXP_B].HeaderText = "MXP-";
            dgv.Columns[(int)Columnas.Corte5_USD_A].HeaderText = "USD+";
            dgv.Columns[(int)Columnas.Corte5_USD_B].HeaderText = "USD-";
            dgv.Columns[(int)Columnas.Corte5_MXP_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte5_MXP_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte5_USD_A].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte5_USD_B].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Corte5_MXP_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte5_MXP_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte5_USD_A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte5_USD_B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Corte5_USD_B].DividerWidth = 2;
        }

        public void FormatoTotales(DataGridView dgv)
        {
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.Columns[(int)ColumnasTotal.Descripcion].Width = dgvSubTotales.Columns[1].Width;
            dgv.Columns[(int)ColumnasTotal.Corte1].Width = dgvSubTotales.Columns[2].Width + dgvSubTotales.Columns[3].Width;
            dgv.Columns[(int)ColumnasTotal.Corte1USD].Width = dgvSubTotales.Columns[4].Width + dgvSubTotales.Columns[5].Width;
            dgv.Columns[(int)ColumnasTotal.Corte2].Width = dgvSubTotales.Columns[6].Width + dgvSubTotales.Columns[7].Width;
            dgv.Columns[(int)ColumnasTotal.Corte2USD].Width = dgvSubTotales.Columns[8].Width + dgvSubTotales.Columns[9].Width;
            dgv.Columns[(int)ColumnasTotal.Corte3].Width = dgvSubTotales.Columns[10].Width + dgvSubTotales.Columns[11].Width;
            dgv.Columns[(int)ColumnasTotal.Corte3USD].Width = dgvSubTotales.Columns[12].Width + dgvSubTotales.Columns[13].Width;
            dgv.Columns[(int)ColumnasTotal.Corte4].Width = dgvSubTotales.Columns[14].Width + dgvSubTotales.Columns[15].Width;
            dgv.Columns[(int)ColumnasTotal.Corte4USD].Width = dgvSubTotales.Columns[16].Width + dgvSubTotales.Columns[17].Width;
            dgv.Columns[(int)ColumnasTotal.Corte5].Width = dgvSubTotales.Columns[18].Width + dgvSubTotales.Columns[19].Width;
            dgv.Columns[(int)ColumnasTotal.Corte5USD].Width = dgvSubTotales.Columns[20].Width + dgvSubTotales.Columns[21].Width;

            dgv.Columns[(int)ColumnasTotal.Corte1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte1USD].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte2USD].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte3USD].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte4USD].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasTotal.Corte5USD].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasTotal.Corte1].Visible = check2.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte2].Visible = check3.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte3].Visible = check4.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte4].Visible = check5.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte5].Visible = check6.Checked == true;

            dgv.Columns[(int)ColumnasTotal.Corte1USD].Visible = check2.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte2USD].Visible = check3.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte3USD].Visible = check4.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte4USD].Visible = check5.Checked == true;
            dgv.Columns[(int)ColumnasTotal.Corte5USD].Visible = check6.Checked == true;

        }
        DataTable _totales = new DataTable();
        public PagosCorte()
        {
            InitializeComponent();

            _totales.Columns.Add("Descripcion", typeof(string));
            _totales.Columns.Add("Corte1A", typeof(decimal));
            _totales.Columns.Add("Corte1B", typeof(decimal));
            _totales.Columns.Add("Corte2A", typeof(decimal));
            _totales.Columns.Add("Corte2B", typeof(decimal));
            _totales.Columns.Add("Corte3A", typeof(decimal));
            _totales.Columns.Add("Corte3B", typeof(decimal));
            _totales.Columns.Add("Corte4A", typeof(decimal));
            _totales.Columns.Add("Corte4B", typeof(decimal));
            _totales.Columns.Add("Corte5A", typeof(decimal));
            _totales.Columns.Add("Corte5B", typeof(decimal));
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            check1.Checked = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            check2.Checked = true;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            check3.Checked = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            check4.Checked = true;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            check5.Checked = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            check6.Checked = true;
        }

        private void check1_Click(object sender, EventArgs e)
        {
            if (!check1.Checked)
            {
                check2.Checked = false;
                check3.Checked = false;
                check4.Checked = false;
                check5.Checked = false;
                check6.Checked = false;
            }
        }

        private void check2_Click(object sender, EventArgs e)
        {
            if (!check2.Checked)
            {
                check3.Checked = false;
                check4.Checked = false;
                check5.Checked = false;
                check6.Checked = false;
            }
        }

        private void check3_Click(object sender, EventArgs e)
        {
            if (!check3.Checked)
            {
                check4.Checked = false;
                check5.Checked = false;
                check6.Checked = false;
            }
        }

        private void check4_Click(object sender, EventArgs e)
        {
            if (!check4.Checked)
            {
                check5.Checked = false;
                check6.Checked = false;
            }

        }

        private void check5_Click(object sender, EventArgs e)
        {
            if (!check5.Checked)
            {
                check6.Checked = false;
            }
        }

        private void check6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.CaragarValores();

                Inicio = dtInicio.Value;
                if (check2.Checked) { Corte1 = dtCorte1.Value; Corte2 = dtCorte1.Value; Corte3 = dtCorte1.Value; Corte4 = dtCorte1.Value; Corte5 = dtCorte1.Value; }
                if (check3.Checked) { Corte1 = dtCorte1.Value; Corte2 = dtCorte2.Value; Corte3 = dtCorte2.Value; Corte4 = dtCorte2.Value; Corte5 = dtCorte2.Value; }
                if (check4.Checked) { Corte1 = dtCorte1.Value; Corte2 = dtCorte2.Value; Corte3 = dtCorte3.Value; Corte4 = dtCorte3.Value; Corte5 = dtCorte3.Value; }
                if (check5.Checked) { Corte1 = dtCorte1.Value; Corte2 = dtCorte2.Value; Corte3 = dtCorte3.Value; Corte4 = dtCorte4.Value; Corte5 = dtCorte4.Value; }
                if (check6.Checked) { Corte1 = dtCorte1.Value; Corte2 = dtCorte2.Value; Corte3 = dtCorte3.Value; Corte4 = dtCorte4.Value; Corte5 = dtCorte5.Value; }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_ReqPagos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Inicio", Inicio);
                        command.Parameters.AddWithValue("@Corte1", Corte1);
                        command.Parameters.AddWithValue("@Corte2", Corte2);
                        command.Parameters.AddWithValue("@Corte3", Corte3);
                        command.Parameters.AddWithValue("@Corte4", Corte4);
                        command.Parameters.AddWithValue("@Corte5", Corte5);

                        DataTable table = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);

                        DataTable Totales = table.Copy();
                        Totales.Rows.Clear();

                        DataRow subtotal = Totales.NewRow();
                        subtotal["Nombre"] = "Subtotal";
                        subtotal["CORTE1_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_A)", string.Empty));
                        subtotal["CORTE1_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_B)", string.Empty));
                        subtotal["CORTE1_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_A)", string.Empty));
                        subtotal["CORTE1_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_B)", string.Empty));

                        subtotal["CORTE2_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_A)", string.Empty));
                        subtotal["CORTE2_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_B)", string.Empty));
                        subtotal["CORTE2_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_A)", string.Empty));
                        subtotal["CORTE2_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_B)", string.Empty));

                        subtotal["CORTE3_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_A)", string.Empty));
                        subtotal["CORTE3_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_B)", string.Empty));
                        subtotal["CORTE3_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_A)", string.Empty));
                        subtotal["CORTE3_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_B)", string.Empty));

                        subtotal["CORTE4_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_A)", string.Empty));
                        subtotal["CORTE4_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_B)", string.Empty));
                        subtotal["CORTE4_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_A)", string.Empty));
                        subtotal["CORTE4_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_B)", string.Empty));

                        subtotal["CORTE5_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_A)", string.Empty));
                        subtotal["CORTE5_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_B)", string.Empty));
                        subtotal["CORTE5_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_A)", string.Empty));
                        subtotal["CORTE5_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_B)", string.Empty));

                        Totales.Rows.Add(subtotal);
                        dgvSubTotales.DataSource = Totales;

                        DataRow total = _totales.NewRow();
                        total["Descripcion"] = "Total general";

                        total["Corte1A"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_A)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_A)", string.Empty)) * TC);
                        total["Corte2A"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_A)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_A)", string.Empty)) * TC);
                        total["Corte3A"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_A)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_A)", string.Empty)) * TC);
                        total["Corte4A"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_A)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_A)", string.Empty)) * TC);
                        total["Corte5A"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_A)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_A)", string.Empty)) * TC);

                        total["Corte1B"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_B)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_B)", string.Empty)) * TC);
                        total["Corte2B"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_B)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_B)", string.Empty)) * TC);
                        total["Corte3B"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_B)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_B)", string.Empty)) * TC);
                        total["Corte4B"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_B)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_B)", string.Empty)) * TC);
                        total["Corte5B"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_B)", string.Empty)) + (Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_B)", string.Empty)) * TC);

                        //total["Corte1MXP"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_B)", string.Empty));
                        //total["Corte2MXP"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_B)", string.Empty));
                        //total["Corte3MXP"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_B)", string.Empty));
                        //total["Corte4MXP"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_B)", string.Empty));
                        //total["Corte5MXP"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_B)", string.Empty));

                        //total["Corte1USD"] = (Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_B)", string.Empty))) * TC;
                        //total["Corte2USD"] = (Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_B)", string.Empty))) * TC;
                        //total["Corte3USD"] = (Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_B)", string.Empty))) * TC;
                        //total["Corte4USD"] = (Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_B)", string.Empty))) * TC;
                        //total["Corte5USD"] = (Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_A)", string.Empty)) + Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_B)", string.Empty))) * TC;
                        _totales.Rows.Add(total);

                        dgvTotales.DataSource = _totales;

                        var query = (from item in table.AsEnumerable()
                                     select item.Field<string>("GroupName")).Distinct();

                        int order = 0;
                        foreach (var item in query.ToList())
                        {
                            DataRow r = table.NewRow();
                            r["Tipo"] = 0;
                            r["Nombre"] = item;
                            r["CORTE1_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_A)", "GroupName='" + item + "'"));
                            r["CORTE1_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_MXP_B)", "GroupName='" + item + "'"));
                            r["CORTE1_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_A)", "GroupName='" + item + "'"));
                            r["CORTE1_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE1_USD_B)", "GroupName='" + item + "'"));

                            r["CORTE2_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_A)", "GroupName='" + item + "'"));
                            r["CORTE2_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_MXP_B)", "GroupName='" + item + "'"));
                            r["CORTE2_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_A)", "GroupName='" + item + "'"));
                            r["CORTE2_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE2_USD_B)", "GroupName='" + item + "'"));

                            r["CORTE3_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_A)", "GroupName='" + item + "'"));
                            r["CORTE3_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_MXP_B)", "GroupName='" + item + "'"));
                            r["CORTE3_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_A)", "GroupName='" + item + "'"));
                            r["CORTE3_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE3_USD_B)", "GroupName='" + item + "'"));

                            r["CORTE4_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_A)", "GroupName='" + item + "'"));
                            r["CORTE4_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_MXP_B)", "GroupName='" + item + "'"));
                            r["CORTE4_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_A)", "GroupName='" + item + "'"));
                            r["CORTE4_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE4_USD_B)", "GroupName='" + item + "'"));

                            r["CORTE5_MXP_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_A)", "GroupName='" + item + "'"));
                            r["CORTE5_MXP_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_MXP_B)", "GroupName='" + item + "'"));
                            r["CORTE5_USD_A"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_A)", "GroupName='" + item + "'"));
                            r["CORTE5_USD_B"] = Convert.ToDecimal(table.Compute("SUM(CORTE5_USD_B)", "GroupName='" + item + "'"));

                            r["GroupName"] = order+ ""+item;
                            table.Rows.Add(r);
                            order++;

                            foreach (DataRow row1 in table.Rows)
                            {
                                if (row1.Field<string>("GroupName").Equals(item.ToString()))
                                    row1.SetField("GroupName",  order+ "1"+item);
                            }
                        }

                        table = (from tv in table.AsEnumerable()
                                         orderby tv.Field<string>("GroupName")
                                         select tv).CopyToDataTable();

                        dgvDatos.DataSource = table;
           
                        DataTable Encabezado = new DataTable();
                        Encabezado.Columns.Add("Tipo", typeof(string));
                        Encabezado.Columns.Add("Nombre", typeof(string));
                        Encabezado.Columns.Add("Corte1", typeof(string));
                        Encabezado.Columns.Add("Corte2", typeof(string));
                        Encabezado.Columns.Add("Corte3", typeof(string));
                        Encabezado.Columns.Add("Corte4", typeof(string));
                        Encabezado.Columns.Add("Corte5", typeof(string));

                        DataRow row = Encabezado.NewRow();
                        row["Tipo"] = "Nombre";
                        row["Nombre"] = "Proveedor";
                        row["Corte1"] = Inicio.ToShortDateString() + " AL " + Corte1.ToShortDateString();
                        row["Corte2"] = Corte1.AddDays(1).ToShortDateString() + " AL " + Corte2.ToShortDateString();
                        row["Corte3"] = Corte2.AddDays(1).ToShortDateString() + " AL " + Corte3.ToShortDateString();
                        row["Corte4"] = Corte3.AddDays(1).ToShortDateString() + " AL " + Corte4.ToShortDateString();
                        row["Corte5"] = Corte4.AddDays(1).ToShortDateString() + " AL " + Corte5.ToShortDateString();

                        Encabezado.Rows.Add(row);

                        dgvHeader.DataSource = Encabezado; 
                        foreach (DataGridViewRow item in dgvHeader.Rows)
                        {
                            item.Height = dgvHeader.Height;
                        }  
                        this.Formato(dgvDatos);
                        this.Formato(dgvSubTotales);
                        this.FormatoTotales(dgvTotales);
                    }
                }        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                dgvHeader.HorizontalScrollingOffset = e.NewValue;
                dgvSubTotales.HorizontalScrollingOffset = e.NewValue;
                dgvTotales.HorizontalScrollingOffset = e.NewValue;
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView dgv = (sender as DataGridView);
            dgvHeader.Columns[1].Width = dgv.Columns[(int)Columnas.Nombre].Width;
            dgvHeader.Columns[2].Width = dgv.Columns[(int)Columnas.Corte1_MXP_A].Width + dgv.Columns[(int)Columnas.Corte1_MXP_B].Width + dgv.Columns[(int)Columnas.Corte1_USD_A].Width + dgv.Columns[(int)Columnas.Corte1_USD_B].Width;
            dgvHeader.Columns[3].Width = dgv.Columns[(int)Columnas.Corte2_MXP_A].Width + dgv.Columns[(int)Columnas.Corte2_MXP_B].Width + dgv.Columns[(int)Columnas.Corte2_USD_A].Width + dgv.Columns[(int)Columnas.Corte2_USD_B].Width;
            dgvHeader.Columns[4].Width = dgv.Columns[(int)Columnas.Corte3_MXP_A].Width + dgv.Columns[(int)Columnas.Corte3_MXP_B].Width + dgv.Columns[(int)Columnas.Corte3_USD_A].Width + dgv.Columns[(int)Columnas.Corte3_USD_B].Width;
            dgvHeader.Columns[5].Width = dgv.Columns[(int)Columnas.Corte4_MXP_A].Width + dgv.Columns[(int)Columnas.Corte4_MXP_B].Width + dgv.Columns[(int)Columnas.Corte4_USD_A].Width + dgv.Columns[(int)Columnas.Corte4_USD_B].Width;
            dgvHeader.Columns[6].Width = dgv.Columns[(int)Columnas.Corte5_MXP_A].Width + dgv.Columns[(int)Columnas.Corte5_MXP_B].Width + dgv.Columns[(int)Columnas.Corte5_USD_A].Width + dgv.Columns[(int)Columnas.Corte5_USD_B].Width;

            

            dgvSubTotales.Columns[(int)Columnas.Nombre].Width = dgv.Columns[(int)Columnas.Nombre].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte1_MXP_A].Width = dgv.Columns[(int)Columnas.Corte1_MXP_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte1_MXP_B].Width = dgv.Columns[(int)Columnas.Corte1_MXP_B].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte1_USD_A].Width = dgv.Columns[(int)Columnas.Corte1_USD_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte1_USD_B].Width = dgv.Columns[(int)Columnas.Corte1_USD_B].Width;

            dgvSubTotales.Columns[(int)Columnas.Corte2_MXP_A].Width = dgv.Columns[(int)Columnas.Corte2_MXP_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte2_MXP_B].Width = dgv.Columns[(int)Columnas.Corte2_MXP_B].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte2_USD_A].Width = dgv.Columns[(int)Columnas.Corte2_USD_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte2_USD_B].Width = dgv.Columns[(int)Columnas.Corte2_USD_B].Width;

            dgvSubTotales.Columns[(int)Columnas.Corte3_MXP_A].Width = dgv.Columns[(int)Columnas.Corte3_MXP_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte3_MXP_B].Width = dgv.Columns[(int)Columnas.Corte3_MXP_B].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte3_USD_A].Width = dgv.Columns[(int)Columnas.Corte3_USD_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte3_USD_B].Width = dgv.Columns[(int)Columnas.Corte3_USD_B].Width;

            dgvSubTotales.Columns[(int)Columnas.Corte4_MXP_A].Width = dgv.Columns[(int)Columnas.Corte4_MXP_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte4_MXP_B].Width = dgv.Columns[(int)Columnas.Corte4_MXP_B].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte4_USD_A].Width = dgv.Columns[(int)Columnas.Corte4_USD_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte4_USD_B].Width = dgv.Columns[(int)Columnas.Corte4_USD_B].Width;

            dgvSubTotales.Columns[(int)Columnas.Corte5_MXP_A].Width = dgv.Columns[(int)Columnas.Corte5_MXP_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte5_MXP_B].Width = dgv.Columns[(int)Columnas.Corte5_MXP_B].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte5_USD_A].Width = dgv.Columns[(int)Columnas.Corte5_USD_A].Width;
            dgvSubTotales.Columns[(int)Columnas.Corte5_USD_B].Width = dgv.Columns[(int)Columnas.Corte5_USD_B].Width;

            dgv.Focus();
            dgv.HorizontalScrollingOffset = 0;

            dgvTotales.Columns[(int)ColumnasTotal.Descripcion].Width = dgvSubTotales.Columns[1].Width;
            dgvTotales.Columns[(int)ColumnasTotal.Corte1].Width = dgvSubTotales.Columns[2].Width + dgvSubTotales.Columns[3].Width;
            dgvTotales.Columns[(int)ColumnasTotal.Corte2].Width = dgvSubTotales.Columns[4].Width + dgvSubTotales.Columns[5].Width;
            dgvTotales.Columns[(int)ColumnasTotal.Corte3].Width = dgvSubTotales.Columns[6].Width + dgvSubTotales.Columns[7].Width;
            dgvTotales.Columns[(int)ColumnasTotal.Corte4].Width = dgvSubTotales.Columns[8].Width + dgvSubTotales.Columns[9].Width;
            dgvTotales.Columns[(int)ColumnasTotal.Corte5].Width = dgvSubTotales.Columns[10].Width + dgvSubTotales.Columns[11].Width;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)Columnas.Tipo].Value) == 1)//linea de proveedor
                    {
                        item.Cells[(int)Columnas.Nombre].Style.Padding = new Padding(20, 0, 0, 0);
                    }
                    if (Convert.ToInt32(item.Cells[(int)Columnas.Tipo].Value) == 0)//encabezado
                    {
                        item.Cells[(int)Columnas.Nombre].Style.Padding = new Padding(1, 0, 0, 0);
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                (sender as DataGridView).CurrentCell = null;

                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                if (e.ColumnIndex == (int)Columnas.Nombre)
                {
                    if (Convert.ToInt32(row.Cells[(int)Columnas.Tipo].Value) == 0)
                    {
                        foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                        {
                            if (Convert.ToString(item.Cells[(int)Columnas.GroupName].Value).Contains(Convert.ToString(row.Cells[(int)Columnas.Nombre].Value)) && Convert.ToInt32(item.Cells[(int)Columnas.Tipo].Value) == 1)
                            {
                                if (item.Visible) item.Visible = false;
                                else item.Visible = true;

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void CaragarValores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Proveedores", string.Empty);
                        command.Parameters.AddWithValue("@GroupCode", 0);

                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                        command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", string.Empty);

                        command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@TC", decimal.Zero);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable ta = new DataTable();
                        da.Fill(ta);

                        TC = (from tc in ta.AsEnumerable()
                              where tc.Field<string>("U_Nombre") == "TC"
                              select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                       

                        txtTC.Text = TC.ToString();

                    
                }
            }
        }

        private void PagosCorte_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CaragarValores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Halconet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
