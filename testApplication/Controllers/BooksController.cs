using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testApplication.Models;

namespace testApplication.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyContext _context;

        // コンテキストを準備
        public BooksController(MyContext context)
        {
            _context = context;
        }

        // GET: Books
        // b.非同期でデータベースにアクセス
        public async Task<IActionResult> Index()
        {
            // a.データベースにアクセス＆取得した結果をリスト化
            return View(await _context.Book.ToListAsync());
        }

        
        // GET: Books/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // b.引数idをキーにデータベースを検索
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            // c.データが見つからなかった場合、404エラー
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            // テーブルから重複のない出版社名を取得
            var list = this._context.Book
                .Select(b => new { Publisher = b.Publisher })
                .Distinct();
            ViewBag.Opts = new SelectList(list, "Publisher", "Publisher");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // a.HTTP POSTで実行されるアクション
        [HttpPost]
        [ValidateAntiForgeryToken]
        // b.ポストデータを引数にバインド
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Publisher,Sample")] Book book)
        {
            if (ModelState.IsValid)
            {
                // c.モデルをデータベースに反映
                _context.Add(book);
                await _context.SaveChangesAsync();
                // d.処理後は一覧画面にリダイレクト
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        // [Edit] リンクから呼び出され、編集フォームを生成
        public async Task<IActionResult> Edit(int? id)
        {
            // 引数idが空の場合は「404 Not Found」
            if (id == null)
            {
                return NotFound();
            }

            // 引数idをキーに書籍情報を取得
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // 編集フォームを表示
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [Save]ボタンで編集内容をデータベースに反映
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Publisher,Sample"/*,RowVersion*/)] Book book)
        {
            // 隠しフィールドのid値と、URLパラメーターのidとが等しいかチェック
            if (id != book.Id)
            {
                return NotFound();
            }

            // 入力値に問題がなければ更新処理
            if (ModelState.IsValid)
            {
                try
                {
                    // a.モデルの更新をデータベースに反映
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                // 競合が発生した場合の処理
                catch (DbUpdateConcurrencyException)
                {
                    // b.書籍が存在しなければ「404 Not Found」
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // ModelState.AddModelError(string.Empty, "競合が検出されました。");
                        // c.書籍が存在する場合は例外
                        throw;
                        // return View(book);
                    }
                }
                // データベース更新に成功した場合はリダイレクト
                return RedirectToAction(nameof(Index));
            }
            // 入力値に問題がある場合には編集フォームを再描画
            return View(book);
        }

        // GET: Books/Delete/5
        // [Delete]リンクから呼び出され、削除フォームを生成
        public async Task<IActionResult> Delete(int? id)
        {
            // 引数idが空の場合は「404 Not Found」
            if (id == null)
            {
                return NotFound();
            }

            // 引数idをキーに書籍情報を取得
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            // 合致する書籍がない場合は「404 Not Found」
            if (book == null)
            {
                return NotFound();
            }
            // 削除フォームを表示
            return View(book);
        }

        // POST: Books/Delete/5
        // a.[Delete]ボタンで削除処理を実行&アクション名を表示
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 引数idをキーに書籍情報を取得
            var book = await _context.Book.FindAsync(id);
            // b.該当するレコードを削除
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            // 削除後は一覧画面にリダイレクト
            return RedirectToAction(nameof(Index));
        }

        // 指定された書籍が存在するかを判定
        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
