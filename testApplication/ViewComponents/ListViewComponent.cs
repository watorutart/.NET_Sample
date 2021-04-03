using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApplication.Models;

namespace testApplication.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        // a.ビューコンポーネントを定義
        private readonly MyContext _context;
        // コンテキストクラスを有効化
        public ListViewComponent(MyContext context)
        {
            this._context = context;
        }

        // b.ビューコンポーネントの実処理
        public async Task<IViewComponentResult> InvokeAsync(int price)
        {
            // c.price円以上の書籍を取得
            return View(await _context.Book
                .Where(b => b.Price >= price).ToListAsync());
        }
    }
}
