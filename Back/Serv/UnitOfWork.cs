using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Vk_server
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        private IRepository<User> _user;
        private IRepository<OAuthClientDetail> _oAuthClientDetail;
        private IRepository<PhotoHuman> _photoHuman;
        private IRepository<UserParameter> _userParameter;
        private IRepository<Clothing> _clothing;
        private IRepository<Sex> _sex;
        private IRepository<Basket> _basket;
        private IRepository<ClothingSize> _clothingSize;
        private IRepository<Shop> _shop;
        private IRepository<RenderPhoto> _renderPhoto;
        private IRepository<Size> _size;

        #endregion

        #region Constructors

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users =>
            _user ?? (_user = new Repository<User>(_context));

        public IRepository<OAuthClientDetail> OAuthClientDetails =>
            _oAuthClientDetail ?? (_oAuthClientDetail = new Repository<OAuthClientDetail>(_context));
        public IRepository<PhotoHuman> PhotoHumans =>
        _photoHuman ?? (_photoHuman = new Repository<PhotoHuman>(_context));
        public IRepository<UserParameter> UserParameters =>
        _userParameter ?? (_userParameter = new Repository<UserParameter>(_context));
        public IRepository<Clothing> Clothings =>
        _clothing ?? (_clothing = new Repository<Clothing>(_context));
        public IRepository<Sex> Sexs =>
        _sex ?? (_sex = new Repository<Sex>(_context));
        public IRepository<Basket> Baskets =>
        _basket ?? (_basket = new Repository<Basket>(_context));
        public IRepository<ClothingSize> ClothingSizes =>
        _clothingSize ?? (_clothingSize = new Repository<ClothingSize>(_context));
        public IRepository<Shop> Shops =>
        _shop ?? (_shop = new Repository<Shop>(_context));

        public IRepository<RenderPhoto> RenderPhotos =>
        _renderPhoto ?? (_renderPhoto = new Repository<RenderPhoto>(_context));

        public IRepository<Size> Sizes =>
        _size ?? (_size = new Repository<Size>(_context));

        #endregion

        #region SaveMethods

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}