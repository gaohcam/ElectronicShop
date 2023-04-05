namespace ElectronicShop_Utility
{
	public class Constants
	{
		// App Settings
		public const string CONNECTION_STRING = "dev_ElectronicShop";
		public const string TOKEN = "Token";
		public const string BASE_ADDRESS = "BaseAddress";
		public const string IMAGE_CONTENT_FOLDER_NAME = "image";
		public const string BEARER = "Bearer";
		public const string DEFAULT_MEDIA_TYPE = "application/json";
		public const string DEFAUT_IMAGE_FILE = "no-image.png";
		public const string PRODUCT_CONTENT_FOLDER_NAME = "product-content";

		// Url page
		public const string PAGE_LOGIN = "/Login";
		public const string PAGE_HOME = "/Home/Index";

		// Default value
		public const string ROLE_NAME_ADMIN = "admin";

		// User
		public const string ERR_USER_NAME_NOT_EXIST = "Tài khoản không tồn tại trong hệ thống";
		public const string ERR_USER_MAIL_NOT_EXIST = "Địa chỉ Email không tồn tại trong hệ thống";
		public const string ERR_USER_NAME_EXISTED = "Tài khoản đã tồn tại trong hệ thống";
		public const string ERR_USER_MAIL_EXISTED = "Địa chỉ Email đã tồn tại trong hệ thống";
		public const string ERR_USER_PHONE_NUMBER_USED = "Số điện thoại đã được sử dụng";
		public const string ERR_USER_PASSWORD_NOT_MATCH = "Mật khẩu và mật khẩu xác nhận không trùng khớp";
		public const string ERR_USER_REGIST_FAILED = "Đăng ký tài khoản không thành công. Vui lòng liên hệ với quản trị viên để được hỗ trợ.";
		public const string ERR_USER_DELETE_FAILED = "Xóa tài khoản không thành công. Vui lòng liên hệ với quản trị viên để được hỗ trợ";
		public const string ERR_USER_UPDATE_FAILED = "Cập nhật tài khoản không thành công. Vui lòng liên hệ với quản trị viên để được hỗ trợ";
		public const string ERR_USER_WRONG_PASSWORD = "Sai mật khẩu !!!";
		public const string ERR_USER_UPDATE_PASSWORD_FAILED = "Cập nhật mật khẩu không thành công";
		public const string ERR_USER_RESTORE_PASSWORD_FAILD = "Khôi phục mật khẩu không thành công";
		public const string ERR_USER_NOT_AUTH = "Tài khoản của bạn chưa được xác thực";
		public const string ERR_USER_LOCKED_ENABLE = "Tài khoản của bạn đã bị khóa";

		// Cateogory
		public const string ERR_CATEGORY_NOT_EXIST = "Không tồn tại danh mục có ID là {0}";
		public const string ERR_CATEGORY_EXISTED = "Danh mục này đã tồn tại trong hệ thống";

		// Product
		public const string ERR_PRODUCT_NOT_EXIST = "Không tồn tại sản phẩm có ID là {0}";
		public const string ERR_PRODUCT_EXISTED = "Sản phẩm này đã tồn tại trong hệ thống";

		//URL Api

		/// <summary>
		/// API_USER
		/// </summary>
		public const string API_USER = "/api/Users/{0}";
		public const string API_USER_UPDATE = "/api/Users/Update/{0}";
		public const string API_USER_DELETE = "/api/Users/Delete/{0}";
		public const string API_USER_ROLE_ASSIGN = "/api/Users/RoleAssign/{0}";
		public const string API_USER_GET_BY_ID = "/api/Users/GetById/{0}";
		public const string API_USER_GET_BY_USER_NAME = "/api/Users/GetByUserName/{0}";
		public const string API_USER_DISABLE_ACCOUNT = "/api/Users/DisableAccount/{0}";

		/// <summary>
		/// API_CATEGORY
		/// </summary>
		public const string API_CATEGORY = "/api/Categories/{0}";
		public const string FIELD_NAME_CATEGORY_CATEGORY_ID = "Mã danh mục";
		public const string FIELD_NAME_CATEGORY_CATEGORY_NAME = "Tên danh mục";
		public const string FIELD_NAME_CATEGORY_DATE_CREATED = "Ngày tạo";
		public const string FIELD_NAME_CATEGORY_DATE_CHANGED = "Ngày thay đổi gần đây";
		public const string FIELD_NAME_CATEGORY_USER_CHANGED = "Người dùng thay đổi gần đây";

		/// <summary>
		/// API_PRODUCT
		/// </summary>
		public const string API_PRODUCT = "/api/Products/{0}";
		public const string FIELD_NAME_PRODUCT_PRODUCT_ID = "Mã sản phẩm";
		public const string FIELD_NAME_PRODUCT_PRODUCT_NAME = "Tên sản phẩm";
		public const string FIELD_NAME_PRODUCT_PRODUCT_INTRODUCTION = "Giới thiệu sản phẩm";
		public const string FIELD_NAME_PRODUCT_PRODUCT_DESCRIPTION = "Thông số sản phẩm";
		public const string FIELD_NAME_PRODUCT_CATEGORY_ID = "Danh mục";
		public const string FIELD_NAME_PRODUCT_STOCK = "Kho";
		public const string FIELD_NAME_PRODUCT_PRODUCT_PRICE = "Giá sản phẩm";
		public const string FIELD_NAME_PRODUCT_PRODUCT_SALE_PRICE = "Giá khuyến mãi sản phẩm";
		public const string FIELD_NAME_PRODUCT_ORIGIN = "Nguồn gốc";
		public const string FIELD_NAME_PRODUCT_PRODUCT_IMAGE = "Hình ảnh sản phẩm";
	}
}
