using System.Data;
using System.Data.Odbc;
using System.Diagnostics;

namespace cs_form_mtn_008_vs2022
{
    public partial class Form2 : Form
    {

        // *****************************
        // DataGridView に必要な設定
        // *****************************
        // dataGridView1.AllowUserToAddRows = false;
        // dataGridView1.AllowUserToDeleteRows = false;
        // dataGridView1.MultiSelect = false;
        // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        // *****************************
        // 更新ボタンの初期プロパティ
        // *****************************
        // 更新ボタン.Enabled = false;

        // *****************************
        // SQL文字列格納用
        // *****************************
        private string query = "select * from 社員マスタ";

        // *****************************
        // 接続文字列作成用
        // *****************************
        private OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();

        // *****************************
        // 接続文字列の準備
        // *****************************
        private void setBuilderData()
        {
            // ドライバ文字列をセット ( 波型括弧{} は必要ありません ) 
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";

            // 接続用のパラメータを追加
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");
        }


        public Form2()
        {
            InitializeComponent();

            setBuilderData();

        }

        // *****************************************
        // ファンクションキ－
        // ( フォームの KeyPreview : True )
        // *****************************************
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // このダイアログを閉じる
                this.Close();
                // このダイアログの終了時のフラグ
                this.DialogResult = DialogResult.OK;
                // 参照する為に modifiers を internal に
                ((Form1)this.Owner).社員コード.Text = "0001";
            }
        }

        // *****************************
        // SELECT 文よりデータ表示
        // *****************************
        private void loadMySQL()
        {

            // 接続と実行用のクラス
            using (OdbcConnection connection = new OdbcConnection())
            using (OdbcCommand command = new OdbcCommand())
            {
                // 接続文字列
                connection.ConnectionString = builder.ConnectionString;

                try
                {
                    // 接続文字列を使用して接続
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // コマンドオブジェクトに接続をセット
                command.Connection = connection;
                // コマンドを通常 SQL用に変更
                command.CommandType = CommandType.Text;

                // *****************************
                // 実行 SQL
                // *****************************
                command.CommandText = query;

                try
                {
                    // レコードセット取得
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        // データを格納するテーブルクラス
                        DataTable dataTable = new DataTable();
                        // DataReader よりデータを格納
                        dataTable.Load(reader);

                        // 画面の一覧表示用コントロールにセット
                        dataGridView1.DataSource = dataTable;

                        // データがあった場合は更新ボタンを使用可能にする
                        if (dataTable.Rows.Count != 0)
                        {
                            // 更新ボタン
                            更新ボタン.Enabled = true;
                        }

                        // リーダを使い終わったので閉じる
                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    // 接続解除
                    connection.Close();
                    MessageBox.Show(ex.Message);
                    return;
                }

                // 接続解除
                connection.Close();
            }

            // カラム幅の自動調整
            dataGridView1.AutoResizeColumns();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            loadMySQL();

            // 主キー部分を編集不可
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void 更新ボタン_Click(object sender, EventArgs e)
        {
            using (OdbcConnection connection = new OdbcConnection())
            {
                // 接続文字列
                connection.ConnectionString = builder.ConnectionString;

                // 更新を管理するクラス
                // ※ 更新するには、テーブルに主キーが必要です
                OdbcDataAdapter adapter = new OdbcDataAdapter();
                // 参照用のマップを追加
                //adapter.TableMappings.Add("Table", "マスタ");
                // 接続とコマンド( コマンドは更新用のレコードを取得した SQL ) 
                adapter.SelectCommand = new OdbcCommand(query, connection);

                // 更新用のオブジェクトを準備
                OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(adapter);
                // 更新用のコマンドを取得
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();

                // 更新用 SQL のベース文字列
                Debug.WriteLine(adapter.UpdateCommand.CommandText);

                // *****************************
                // 更新
                // *****************************
                try
                {
                    // DataGridView の内容を使用して更新
                    // ※ 他で更新されてしまった行は更新エラーとなります
                    // ※ 内部で管理された更新予定行を対象として更新します
                    adapter.Update((DataTable)dataGridView1.DataSource);

                    MessageBox.Show("更新を終了しました");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
