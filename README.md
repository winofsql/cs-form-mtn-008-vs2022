# cs-form-mtn-008-vs2022

### 一覧( DataGridView ) による更新処理

![image](https://github.com/winofsql/cs-form-mtn-008-vs2022/assets/1501327/a0ece432-e13e-4384-b3c9-492a6886919c

- ### 画面コントロール
  - 更新ボタンの初期設定は Enabled = false
  - SQL は order by で 主キー順に並べる
  - データがあった場合のみ更新ボタンを使用可にする
  - DataGridView は編集のみ可とする
  - DataGridView の複数選択は不可とする
  - DataGridView の行選択は全ての列とする
  - ESC キーでフォームを閉じて、Form1 の社員コードに現在の最大値 + 1 を編集したものをセットする
  - セット可能にする為に社員コードの modifiers を internal とする

```cs
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

        // 現在の最大値を取得して、Form1 に +1 して渡す
        int rows = dataGridView1.RowCount;
        string? code = dataGridView1.Rows[rows - 1].Cells[0].Value.ToString();
        int num = Int16.Parse(code);
        num++;
        ((Form1)this.Owner).社員コード.Text = $"{num:0000}";
    }
}
```
