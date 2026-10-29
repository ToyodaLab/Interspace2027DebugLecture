# Disaster — Debugging Lab

## English

### 1. Get the project
Clone the repository or download the ZIP and unzip it. In **Unity Hub**, choose **Add → Add project from disk** and select the folder. If Hub asks about the Unity version, choose the closest installed version and confirm the upgrade.

### 2. Opening with errors
Unity will say the project has compilation errors and offer **Safe Mode**.
- **Enter Safe Mode**: the Editor opens so you can fix the scripts first.
- **Ignore**: the Editor opens, but Play is blocked until the errors are fixed.

Either is fine.

### 3. Fix the compiler errors first
1. Open the Console (**Window → General → Console**).
2. Read the first red error: file name, line number, message.
3. Double-click it, fix the script, save, go back to Unity and wait for it to recompile.
4. Repeat until there are no red errors.

### 4. Open the scene
Open `Assets/Scenes/Disaster`.

### 5. Press Play and fix the runtime problems
Controls: **Arrow keys** move · **Space** jump · **E** use the switch · **5** countdown.
**Save before pressing 5.** The Console will be very noisy at first. Find out why.

### 6. The debugging loop
**Reproduce → Isolate → Inspect → Fix → Confirm.** Change one thing at a time.

### 7. Debugger setup
1. Set the External Script Editor in **Edit → Preferences → External Tools** (Visual Studio, VS Code with the Unity extension, or Rider).
2. Switch Unity to **Debug mode** (bug icon, bottom-right of the Editor).
3. Attach to Unity, set a breakpoint, press Play, then Step Over / Step Into and read the Locals.

### 8. Done when
Play runs with a quiet Console, the player moves and jumps, the yellow cube spins, the lamp toggles with **E**, and the goal logs when you touch it.

---

## 日本語

### 1. プロジェクトを入手する
リポジトリをクローンするか、ZIP を展開します。**Unity Hub** で **Add → Add project from disk** を選び、フォルダーを指定します。Unity のバージョンを聞かれたら、インストール済みで最も近いものを選んでアップグレードを確認します。

### 2. エラーがある状態で開く
Unity が「コンパイルエラーがある」と表示し、**Safe Mode** を提案します。
- **Enter Safe Mode**：エディターが開くので、先にスクリプトを直します。
- **Ignore**：エディターは開きますが、エラーを直すまで Play できません。

どちらでも構いません。

### 3. まずコンパイルエラーを直す
1. Console を開きます（**Window → General → Console**）。
2. 最初の赤いエラーを読みます：ファイル名、行番号、メッセージ。
3. ダブルクリックしてスクリプトを直し、保存して Unity に戻り、再コンパイルを待ちます。
4. 赤いエラーがなくなるまで繰り返します。

### 4. シーンを開く
`Assets/Scenes/Disaster` を開きます。

### 5. Play して実行時の問題を直す
操作：**矢印キー** 移動 · **Space** ジャンプ · **E** スイッチ · **5** カウントダウン。
**5 を押す前に保存してください。** 最初は Console がとても騒がしくなります。その理由を調べましょう。

### 6. デバッグの流れ
**再現 → 切り分け → 調査 → 修正 → 確認。** 一度に変更するのは 1 つだけ。

### 7. デバッガーの設定
1. **Edit → Preferences → External Tools** で External Script Editor を設定します（Visual Studio、Unity 拡張機能付き VS Code、Rider）。
2. Unity を **Debug モード**にします（エディター右下の虫のアイコン）。
3. Unity にアタッチし、ブレークポイントを設定して Play し、ステップオーバー／ステップインで Locals を確認します。

### 8. 完了の条件
Console が静かな状態で Play でき、プレイヤーが移動・ジャンプし、黄色いキューブが回り、**E** でランプが切り替わり、ゴールに触れるとログが出れば完了です。
