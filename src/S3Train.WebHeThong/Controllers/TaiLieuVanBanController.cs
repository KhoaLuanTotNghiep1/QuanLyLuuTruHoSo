﻿using Microsoft.AspNet.Identity;
using S3Train.Contract;
using S3Train.Core.Constant;
using S3Train.Domain;
using S3Train.WebHeThong.CommomClientSide.DropDownList;
using S3Train.WebHeThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlgorithmLibrary.Kmeans;
using X.PagedList;
using S3Train.WebHeThong.CommomClientSide.Function;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using S3Train.Core.Enum;
using S3Train.Core.Extension;

namespace S3Train.WebHeThong.Controllers
{
    [Authorize]
    [RoutePrefix("Tai-Lieu-Van-Ban")]
    public class TaiLieuVanBanController : Controller
    {
        private readonly ITaiLieuVanBanService _taiLieuVanBanService;
        private readonly INoiBanHanhService _noiBanHanhService;
        private readonly IHoSoService _hoSoService;
        private readonly ILoaiHoSoService _loaiHoSoService;
        private readonly IUserService _userService;
        private readonly IChiTietMuonTraService _chiTietMuonTraService;
        private readonly IFunctionLichSuHoatDongService _functionLichSuHoatDongService;
        private readonly IHinhVanBanService _hinhVanBanService;

        public TaiLieuVanBanController()
        {

        }

        public TaiLieuVanBanController(ITaiLieuVanBanService taiLieuVanBanService, INoiBanHanhService noiBanHanhService, 
            IHoSoService hoSoService, ILoaiHoSoService loaiHoSoService, IUserService userService, 
            IFunctionLichSuHoatDongService functionLichSuHoatDongService, IChiTietMuonTraService chiTietMuonTraService,
            IHinhVanBanService hinhVanBanService)
        {
            _taiLieuVanBanService = taiLieuVanBanService;
            _noiBanHanhService = noiBanHanhService;
            _hoSoService = hoSoService;
            _loaiHoSoService = loaiHoSoService;
            _userService = userService;
            _chiTietMuonTraService = chiTietMuonTraService;
            _functionLichSuHoatDongService = functionLichSuHoatDongService;
            _hinhVanBanService = hinhVanBanService;
        }

        // GET: TaiLieuVanBan
        [Route("Danh-Sach")]
        public ActionResult Index(int? pageIndex, int? pageSize, string searchString, 
            string dang, bool active = true)
        {
            pageIndex = (pageIndex ?? 1);
            pageSize = pageSize ?? GlobalConfigs.DEFAULT_PAGESIZE;

            var model = new TaiLieuVanBanIndexViewModel()
            {
                PageIndex = pageIndex.Value,
                PageSize = pageSize.Value
            };

            var taiLieuVanBans = _taiLieuVanBanService.GetAllPaged(pageIndex, pageSize.Value, p => p.TrangThai == active && 
                p.Dang.Contains(dang), p => p.OrderByDescending(c => c.NgayTao));

            if (!string.IsNullOrEmpty(searchString))
            {
                taiLieuVanBans = _taiLieuVanBanService.GetAllPaged(pageIndex, pageSize.Value, p => p.Ten.Contains(searchString) || p.Loai.Contains(searchString)
                    || p.NoiDung.Contains(searchString), p => p.OrderByDescending(c => c.NgayTao));
            }

            model.Paged = taiLieuVanBans;
            model.Items = GetTaiLieuVanBans(taiLieuVanBans.ToList());

            ViewBag.Active = active;
            ViewBag.searchString = searchString;
            ViewBag.Controller = "TaiLieuVanBan";
            ViewBag.Dang = dang;

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(string id)
        {
            var model = new TaiLieu_VanBanViewModel();
            var dangs = new List<object> { GlobalConfigs.DANG_DEN, GlobalConfigs.DANG_NOIBO };

            DropDowmn();

            if (string.IsNullOrEmpty(id))
            {
                return View(model);
            }
            else
            {
                var taiLieuVanBan = _taiLieuVanBanService.Get(m => m.Id == id);
                model = GetTaiLieuVanBan(taiLieuVanBan);
                return View(model);
            }
        }

        public void DropDowmn()
        {
            var dangs = new List<object> { GlobalConfigs.DANG_DEN, GlobalConfigs.DANG_NOIBO };

            ViewBag.LoaiHoSos = SelectListItemFromDomain.SelectListItem_LoaiHoSo(_loaiHoSoService.GetAll(m => m.OrderBy(t => t.Ten)));
            ViewBag.NoiBanHanhs = SelectListItemFromDomain.SelectListItem_NoiBanHanh(_noiBanHanhService.GetAll(m => m.OrderBy(t => t.Ten)));
            ViewBag.Dangs = SelectListItemFromDomain.SelectListItem_Object(dangs);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(TaiLieu_VanBanViewModel model, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> images)
        {
            var taiLieuVanBan = string.IsNullOrEmpty(model.Id) ? new TaiLieuVanBan { NgayCapNhat = DateTime.Now }
                : _taiLieuVanBanService.Get(m => m.Id == model.Id);

            var autoList = AutoCompleteTextHoSos(GetHoSos());
            string userId = User.Identity.GetUserId();
            string cthd = model.Loai + ": " + model.Ten;

            #region File
            string localFile = Server.MapPath("~/Content/HoSo/");
            string localImage = Server.MapPath("~/Content/HinhAnhTLVB/");

            string path = UpFileGetPathOrFileName(file, localFile, model.DuongDan, "path");
            string hinhAnh = UpFileGetPathOrFileName(images.ElementAt(0), localImage, model.HinhAnh);
            #endregion

            #region taiLieuVanBan
            taiLieuVanBan.Dang = model.Dang;
            taiLieuVanBan.DuongDan = path;
            taiLieuVanBan.GhiChu = model.GhiChu;
            taiLieuVanBan.HoSoId = autoList.FirstOrDefault(p => p.Text == model.HoSoId).Id;
            taiLieuVanBan.Loai = model.Loai;
            taiLieuVanBan.NguoiDuyet = model.NguoiDuyet;
            taiLieuVanBan.NoiDung = model.NoiDung;
            taiLieuVanBan.NguoiGuiHoacNhan = model.NguoiGuiHoacNhan;
            taiLieuVanBan.NguoiKy = model.NguoiKy;
            taiLieuVanBan.NoiBanHanhId = model.NoiBanHanhId;
            taiLieuVanBan.NoiNhan = model.NoiNhan;
            taiLieuVanBan.SoKyHieu = model.SoKyHieu;
            taiLieuVanBan.SoTo = model.SoTo;
            taiLieuVanBan.Ten = model.Ten;
            taiLieuVanBan.NgayBanHanh = model.NgayBanHanh;
            taiLieuVanBan.TinhTrang = EnumTinhTrang.TrongKho;
            taiLieuVanBan.UserId = userId;
            taiLieuVanBan.HinhAnh = hinhAnh;
            #endregion

            #region Create or update tlvb
            if (string.IsNullOrEmpty(model.Id))
            {
                var checkName = _taiLieuVanBanService.Get(m => m.Ten == model.Ten);

                if (checkName != null)
                {
                    DropDowmn();
                    TempData["AlertMessage"] = "Văn Bản Có Cùng Tên Đã Tồn Tại";
                    return View(model);
                }

                _taiLieuVanBanService.Insert(taiLieuVanBan);
                _functionLichSuHoatDongService.Create(ActionWithObject.Create, userId, cthd);
                TempData["AlertMessage"] = "Tạo Mới Thành Công";
            }
            else
            {
                _taiLieuVanBanService.Update(taiLieuVanBan);
                _functionLichSuHoatDongService.Create(ActionWithObject.Update, userId, cthd);
                TempData["AlertMessage"] = "Cập Nhật Thành Công";
            }
            #endregion

            if(images.Count() > 0)
            {
                var listImage = UpManyFile(images, localImage);
                AddImagesForItem(taiLieuVanBan.Id, listImage);
            }

            return RedirectToAction("Index", new { dang =  model.Dang});
        }

        [Authorize(Roles = GlobalConfigs.ROLE_GIAMDOC_CANBOVANTHU)]
        [HttpPost]
        public async Task<ActionResult> ChangeDangForTLVB(TaiLieu_VanBanViewModel model)
        {
            var taiLieuVanBan = _taiLieuVanBanService.Get(p => p.Id == model.Id);
            var user = await _userService.GetUserById(User.Identity.GetUserId());

            taiLieuVanBan.NguoiGuiHoacNhan = user.FullName;
            taiLieuVanBan.Dang = GlobalConfigs.DANG_DI;
            taiLieuVanBan.NoiNhan = model.NoiNhan;
            taiLieuVanBan.TinhTrang = EnumTinhTrang.DaGoi;

            _taiLieuVanBanService.Update(taiLieuVanBan);
            _functionLichSuHoatDongService.Create(ActionWithObject.ChangeStatus, User.Identity.GetUserId(),"gởi văn bản đi: " + model.Ten);

            TempData["AlertMessage"] = "Gởi văn bản thành công";
            return RedirectToAction("Index", new { dang = GlobalConfigs.DANG_DI });
        }

        [Authorize(Roles = GlobalConfigs.ROLE_GIAMDOC_CANBOVANTHU)]
        public ActionResult Delete(string id)
        {
            var taiLieuVanBan = _taiLieuVanBanService.Get(m => m.Id == id);

            var count = _chiTietMuonTraService.Gets(p => p.TaiLieuVanBanId == id).Count();

            if (count > 0)
            {
                TempData["AlertMessage"] = "Không Thể Xóa Vì Có " + count + " Chi Tiết Mượn Trả Phụ Thuộc";
                return RedirectToAction("Index", new { active = false });
            }

            _taiLieuVanBanService.Remove(taiLieuVanBan);
            TempData["AlertMessage"] = "Xóa Thành Công";
            _functionLichSuHoatDongService.Create(ActionWithObject.Delete, User.Identity.GetUserId(), taiLieuVanBan.Loai + ": " + taiLieuVanBan.Ten);
            return RedirectToAction("Index", new { dang = taiLieuVanBan.Dang});
        }

        [Route("Thong-Tin-Chi-Tiet")]
        public ActionResult Detail(string id)
        {
            var model = GetTaiLieuVanBan(_taiLieuVanBanService.GetByIdHaveJoin(id));

            model.hinhVanBans = GetHinhVanBans(id);

            return View(model);
        }

        public ActionResult DeleteImage(string id, string vbId)
        {
            var image = _hinhVanBanService.GetById(id);
            if (image != null)
            {
                _hinhVanBanService.Remove(image);
            }

            return RedirectToAction("Detail", new { id = vbId});
        }

        [Authorize(Roles = GlobalConfigs.ROLE_GIAMDOC_CANBOVANTHU)]
        public ActionResult ChangeActive(string id, bool active)
        {
            var model = _taiLieuVanBanService.Get(m => m.Id == id);

            model.TrangThai = active;

            _taiLieuVanBanService.Update(model);
            _functionLichSuHoatDongService.Create(ActionWithObject.ChangeStatus, User.Identity.GetUserId(),
                model.Loai + ": " + model.Ten + " thành "+ active);

            var messenge = Messenger.ChangeActiveMessenge(active);

            TempData["AlertMessage"] = messenge;

            return RedirectToAction("Index", new { dang = model.Dang});
        }

        [HttpPost]
        public ActionResult AutoCompleteText(string text)
        {
            var model = AutoCompleteTextHoSos(GetHoSos());

            model = model.Where(p => p.Text.Contains(text)).ToHashSet();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StorageSuggestion(string document, string type)
        {
            string local = "Không tìm thấy tài liêu/văn bản có cùng nội dung! Tạo hồ sơ mới.";

            var hosos = AutoCompleteTextHoSos(GetHoSos());

            var list = _taiLieuVanBanService.GetDocuments().Take(50).ToList();

            var docCollection = new DocumentCollection()
            {
                DocumentList = list
            };
            list.Add(document);

            var cluster = _taiLieuVanBanService.CountDocumentType(type);

            List<DocumentVector> vSpace = VectorSpaceModel.ProcessDocumentCollection(docCollection); // retuen ds vector
            List<Centroid> resultSet = DocumnetClustering.DocumentCluster(cluster, vSpace, document);

            string documentNeedSearch = DocumnetClustering.FindClosestDocument();

            if(!string.IsNullOrEmpty(documentNeedSearch))
            {
                var taiLieuVanBan = _taiLieuVanBanService.Get(p => p.Ten == documentNeedSearch);

                local = hosos.FirstOrDefault(p => p.Id == taiLieuVanBan.HoSoId).Text;
            }

            return Json(new { da = local}, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TestAlgorithm()
        {
            TestAlgorithmModel model = new TestAlgorithmModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult TestAlgorithm(TestAlgorithmModel model)
        {
            var list = _taiLieuVanBanService.GetDocuments().Take(model.Amount).ToList();

            list.Add(model.Name);

            var docCollection = new DocumentCollection()
            {
                DocumentList = list
            };

            List<DocumentVector> vSpace = VectorSpaceModel.ProcessDocumentCollection(docCollection);
            List<Centroid> resultSet = DocumnetClustering.DocumentCluster(model.Cluster, vSpace, model.Name);
            string s = DocumnetClustering.FindClosestDocument();

            var mode = new TestAlgorithmModel
            {
                Name = model.Name,
                Amount = model.Amount,
                Cluster = model.Cluster,
                Centroids = resultSet,
                DocumentNear = s
            };

            return View(mode);
        }

        public List<HinhVanBan> GetHinhVanBans(string id)
        {
            var list = new List<HinhVanBan>();
            list = _hinhVanBanService.Gets(p => p.TaiLieuVanBanId == id).ToList();
            return list;
        }


        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="a"></param>
        /// <param name="url">path folder</param>
        /// <param name="get">path/fileName</param>
        /// <returns>path/file name</returns>
        public string UpFileGetPathOrFileName(HttpPostedFileBase a, string url, string name, string get = "fileName")
        {
            string fileName = name;
            if (a != null && a.ContentLength > 0)
            {
                fileName = Path.GetFileName(a.FileName).ToString();
                string path = Path.Combine(url, fileName);
                a.SaveAs(path);

                if (get == "path")
                    return path;
                else
                    return fileName;
            }
            else
            {
                return fileName;
            }
        }

        public List<string> UpManyFile(IEnumerable<HttpPostedFileBase> a, string url)
        {
            var list = new List<string>();

            foreach (var file in a)
            {
                string fileName = "";
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName).ToString();
                    string path = Path.Combine(url, fileName);
                    file.SaveAs(path);

                    list.Add(fileName);
                }
            }

            return list;
        }

        public void AddImagesForItem(string id, List<string> nameImages)
        {
            HinhVanBan hinhVanBan;

            foreach (var image in nameImages)
            {
                hinhVanBan = new HinhVanBan
                {
                    Id = Guid.NewGuid().ToString(),
                    NgayTao = DateTime.Now,
                    TenHinh = image,
                    TrangThai = true,
                    TaiLieuVanBanId = id
                };

                _hinhVanBanService.Insert(hinhVanBan);
            }
        }

        private HashSet<AutoCompleteTextModel> AutoCompleteTextHoSos(IList<HoSo> hoSos)
        {
            var list = ConvertDomainToAutoCompleteModel.LocalTaiLieu(hoSos);

            return list;
        }

        private IList<HoSo> GetHoSos()
        {
            var listKe = _hoSoService.GetAllHaveJoinHoSo();

            var model = listKe.Where(p => p.TrangThai == true);

            return model.ToList();
        }

        private TaiLieu_VanBanViewModel GetTaiLieuVanBan(TaiLieuVanBan x)
        {
            var autoList = AutoCompleteTextHoSos(GetHoSos());

            var model = new TaiLieu_VanBanViewModel
            {
               Id = x.Id,
               Dang = x.Dang,
               DuongDan = x.DuongDan,
               GhiChu = x.GhiChu,
               HoSo = x.HoSo,
               Loai = x.Loai,
               NgayCapNhat =  x.NgayCapNhat,
               NgayTao = x.NgayTao,
               NguoiDuyet = x.NguoiDuyet,
               NoiDung = x.Dang.GetDecription(),
               NguoiGuiHoacNhan = x.NguoiGuiHoacNhan,
               NguoiKy = x.NguoiKy,
               NoiBanHanh = x.NoiBanHanh,
               NoiBanHanhId = x.NoiBanHanhId,
               NoiNhan = x.NoiNhan,
               SoKyHieu = x.SoKyHieu,
               SoTo = x.SoTo,
               Ten = x.Ten,
               TinhTrang = x.TinhTrang,
               TrangThai = x.TrangThai,
               User = x.User,
               UserId = x.UserId,
               NgayBanHanh = x.NgayBanHanh,
               HinhAnh = x.HinhAnh,
               HoSoId = autoList.FirstOrDefault(p => p.Id == x.HoSoId).Text
            };

            return model;
        }

        private List<TaiLieu_VanBanViewModel> GetTaiLieuVanBans(IList<TaiLieuVanBan> taiLieuVanBans)
        {
            var autoList = AutoCompleteTextHoSos(GetHoSos());

            return taiLieuVanBans.Select(x => new TaiLieu_VanBanViewModel
            {
                Id = x.Id,
                NgayTao = x.NgayTao,
                Ten = x.Ten,
                TinhTrang = x.TinhTrang,
                TrangThai = x.TrangThai,
                Dang = x.Dang,
                HoSoId = autoList.FirstOrDefault(p => p.Id == x.HoSoId).Text
            }).ToList();
        }
    }
}