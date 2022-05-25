//using PreSchool.Application.Exceptions;
//using PreSchool.Application.Infastructures;
//using PreSchool.Application.Services.Addresses.Models.Filters;
//using PreSchool.Application.Services.AppConfigurations;
//using PreSchool.Application.Services.AppUsers.Models.Commands;
//using PreSchool.Application.Services.AppUsers.Models.Dtos;
//using PreSchool.Data.Entities.AppUsers;
//using PreSchool.Data.Enumerations;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace PreSchool.Application.Services.AppUsersDepartments
//{
//    public class AppUserDepartmentService : IAppUserDepartmentService
//    {
//        private readonly IAppFeatureService _featureService;
//        private readonly IApplicationDbContext _context;
//        private readonly ICurrentUserService _currentUserService;

//        public AppUserDepartmentService(
//            IAppFeatureService featureService,
//            IApplicationDbContext context,
//            ICurrentUserService currentUserService
//            )
//        {
//            _featureService = featureService;
//            _context = context;
//            _currentUserService = currentUserService;
//        }

//        #region AppUser Departments
//        public async Task<int> InsertUpdateAppUserDepartment(InsertUpdateDepartment command)
//        {
//            //// Check for appUser type
//            //if (command.LimitedToAppUserTypeId != (int)_currentUserService.AppUserType)
//            //{
//            //    if (command.LimitedToAppUserTypeId == (int)AppUserTypeEnum.Internal)
//            //        throw new BadRequestException("Only internal user can add department");

//            //    if (command.LimitedToAppUserTypeId == (int)AppUserTypeEnum.StoreUser
//            //        && _currentUserService.AppUserType != AppUserTypeEnum.Internal)
//            //        throw new BadRequestException(" Only store user can add department of store");
//            //}
//            // New department
//            if (command.Id == 0)
//            {
//                var department = new AppUserDepartment
//                {
//                    Description = command.Description,
//                    LimitedToAppUserTypeId = (int)_currentUserService.AppUserType,
//                    LimitedToStores = _currentUserService.AppUserType != AppUserTypeEnum.Internal,
//                    LimitedToVendors = _currentUserService.AppUserType == AppUserTypeEnum.Vendor || _currentUserService.AppUserType == AppUserTypeEnum.VendorAssociateUser,
//                    Name = command.Name,
//                    DisplayOrder = command.DisplayOrder,

//                };
//                _context.AppUserDepartments.Add(department);
//                await _context.SaveChangesAsync();

//                // Store mapping
//                if (_currentUserService.StoreId != null)
//                    _context.StoreMappings.Add(new Data.Entities.Stores.StoreMapping
//                    {
//                        EntityId = department.Id,
//                        EntityName = nameof(AppUserDepartment),
//                        StoreId = _currentUserService.StoreId ?? 0
//                    });

//                // Vendor mapping
//                if (_currentUserService.AppUserType == AppUserTypeEnum.Vendor || _currentUserService.AppUserType == AppUserTypeEnum.VendorAssociateUser && _currentUserService.VendorId != null)
//                {
//                    _context.VendorMappings.Add(new Data.Entities.Vendors.VendorMapping
//                    {
//                        EntityId = department.Id,
//                        EntityName = nameof(AppUserDepartment),
//                        VendorId = _currentUserService.VendorId ?? 0
//                    });
//                }
//                await _context.SaveChangesAsync();
//                return department.Id;
//            }


//            // Get department
//            var existingDepartment = await _context.AppUserDepartments
//                                .FirstOrDefaultAsync(d => d.Id == command.Id);
//            if (existingDepartment == null)
//                throw new NotFoundException("Invalid department", "Department not found");
//            existingDepartment.Description = command.Description;
//            existingDepartment.Name = command.Name;
//            existingDepartment.DisplayOrder = command.DisplayOrder;

//            await _context.SaveChangesAsync();
//            return existingDepartment.Id;

//        }

//        public async Task<List<AppUserDepartmentDto>> AppUserDepartments(DepartmentFilter filter)
//        {
//            var appUserType = (int)_currentUserService.AppUserType;
//            var departments = _context.AppUserDepartments
//                                .AsNoTracking();
//            // .Where(d => d.LimitedToAppUserTypeId == appUserType) ;

//            // initilize filter if filter is null
//            if (filter == null)
//                filter = new DepartmentFilter();

//            // Check if department for particular user type is requested or not, if not then 
//            // return login user typed departments
//            if (!filter.AppUserTypeId.HasValue)
//                departments = departments.Where(d => d.LimitedToAppUserTypeId == appUserType);

//            // Check for paermission

//            // For internal user, internal user can view all the departments
//            if (_currentUserService.AppUserType == AppUserTypeEnum.Internal)
//            {
//                if (filter.AppUserTypeId.HasValue)
//                    departments = departments.Where(d => d.LimitedToAppUserTypeId == filter.AppUserTypeId);

//                if (filter.StoreId.HasValue)
//                    departments = from c in departments
//                                  join sm in _context.StoreMappings
//                                on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                                  from sm in c_sm.DefaultIfEmpty()
//                                  where !c.LimitedToStores || filter.StoreId == sm.StoreId
//                                  select c;

//                if (filter.VendorId.HasValue)
//                    departments = from c in departments
//                                  join sm in _context.VendorMappings
//                                on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                                  from sm in c_sm.DefaultIfEmpty()
//                                  where !c.LimitedToVendors || filter.VendorId == sm.VendorId
//                                  select c;

//            }

//            // For Store user, Can view all the stores and vendor department
//            else if (_currentUserService.AppUserType == AppUserTypeEnum.StoreUser)
//            {
//                if (filter.AppUserTypeId.HasValue)
//                {
//                    // Store user cannot access internal user department
//                    if (filter.AppUserTypeId != (int)AppUserTypeEnum.Internal)
//                        departments = departments.Where(d => d.LimitedToAppUserTypeId == filter.AppUserTypeId);
//                }

//                departments = from c in departments
//                              join sm in _context.StoreMappings
//                            on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                              from sm in c_sm.DefaultIfEmpty()
//                              where !c.LimitedToStores || (filter.StoreId.HasValue && sm.StoreId == filter.StoreId)
//                                || (!filter.StoreId.HasValue && sm.StoreId == _currentUserService.StoreId)
//                              select c;


//                if (filter.VendorId.HasValue)
//                    departments = from c in departments
//                                  join sm in _context.VendorMappings
//                                on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                                  from sm in c_sm.DefaultIfEmpty()
//                                  where !c.LimitedToVendors || filter.VendorId == sm.VendorId
//                                  select c;

//            }
//            else if (_currentUserService.AppUserType == AppUserTypeEnum.Vendor)
//            {
//                // Vendor mapping
//                if (_currentUserService.VendorId != null)
//                {
//                    departments = from c in departments
//                                  join sm in _context.VendorMappings
//                                on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                                  from sm in c_sm.DefaultIfEmpty()
//                                  where !c.LimitedToVendors || _currentUserService.VendorId == sm.VendorId
//                                  select c;
//                }
//            }

//            return await departments
//                    .OrderBy(d => d.DisplayOrder)
//                    .Select(d => new AppUserDepartmentDto
//                    {
//                        Description = d.Description,
//                        DisplayOrder = d.DisplayOrder,
//                        Id = d.Id,
//                        Name = d.Name
//                    })
//                    .ToListAsync();
//        }


//        #region Internal user department

//        public async Task<List<AppUserDepartmentDto>> InternalUserDepartments()
//        {
//            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal)
//                throw new ForbiddenException();

//            if (!_currentUserService.HavePermission(Permissions.InternalDepartmentManagement))
//                throw new ForbiddenException();

//            return await _context.AppUserDepartments
//                                .AsNoTracking()
//                                .Where(d => d.LimitedToAppUserTypeId == (int)_currentUserService.AppUserType)
//                                .OrderBy(d => d.DisplayOrder)
//                                .Select(d => new AppUserDepartmentDto
//                                {
//                                    Description = d.Description,
//                                    DisplayOrder = d.DisplayOrder,
//                                    Id = d.Id,
//                                    Name = d.Name
//                                })
//                                .ToListAsync();
//        }

//        public async Task<int> InsertUpdateInternalAppUserDepartment(InsertUpdateDepartment command)
//        {
//            //// Check for appUser type

//            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal)
//                throw new ForbiddenException();

//            if (!_currentUserService.HavePermission(Permissions.InternalDepartmentManagement))
//                throw new ForbiddenException();

//            // New department
//            if (command.Id == 0)
//            {
//                var department = new AppUserDepartment
//                {
//                    Description = command.Description,
//                    LimitedToAppUserTypeId = (int)_currentUserService.AppUserType,
//                    LimitedToStores = false,
//                    LimitedToVendors = false,
//                    Name = command.Name,
//                    DisplayOrder = command.DisplayOrder,

//                };
//                _context.AppUserDepartments.Add(department);

//                await _context.SaveChangesAsync();
//                return department.Id;
//            }


//            // Get department
//            var existingDepartment = await _context.AppUserDepartments
//                                .FirstOrDefaultAsync(d => d.Id == command.Id);
//            if (existingDepartment == null)
//                throw new NotFoundException("Invalid department", "Department not found");
//            existingDepartment.Description = command.Description;
//            existingDepartment.Name = command.Name;
//            existingDepartment.DisplayOrder = command.DisplayOrder;

//            await _context.SaveChangesAsync();
//            return existingDepartment.Id;

//        }

//        #endregion

//        #region Store department

//        public async Task<List<AppUserDepartmentDto>> StoreDepartments(int storeId)
//        {

//            if (!_currentUserService.HavePermission(Permissions.StoreDepartmentManagement))
//            {
//                if (storeId != _currentUserService.StoreId)
//                    throw new ForbiddenException();
//            }

//            var departments = _context.AppUserDepartments
//                                     .AsNoTracking()
//                                     .Where(d => d.LimitedToAppUserTypeId == (int)AppUserTypeEnum.StoreUser);

//            // Store mapping
//            departments = from c in departments
//                          join sm in _context.StoreMappings
//                        on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                          from sm in c_sm.DefaultIfEmpty()
//                          where !c.LimitedToStores || sm.StoreId == storeId
//                          select c;

//            return await departments.OrderBy(d => d.DisplayOrder)
//                                .Select(d => new AppUserDepartmentDto
//                                {
//                                    Description = d.Description,
//                                    DisplayOrder = d.DisplayOrder,
//                                    Id = d.Id,
//                                    Name = d.Name
//                                })
//                                .ToListAsync();
//        }

//        public async Task<int> InsertUpdateStoreAppUserDepartment(InsertUpdateStoreDepartment command)
//        {
//            //// Check for appUser type

//            if (_currentUserService.AppUserType != AppUserTypeEnum.Internal && _currentUserService.AppUserType != AppUserTypeEnum.StoreUser)
//                throw new ForbiddenException();

//            if (!_currentUserService.HavePermission(Permissions.StoreDepartmentManagement))
//                throw new ForbiddenException();

//            // New department
//            AppUserDepartment department;
//            if (command.Id == 0)
//            {
//                department = new AppUserDepartment
//                {
//                    Description = command.Description,
//                    LimitedToAppUserTypeId = (int)AppUserTypeEnum.StoreUser,
//                    LimitedToStores = command.LimitedToStores?.LimitedToStores ?? false,
//                    LimitedToVendors = _currentUserService.AppUserType == AppUserTypeEnum.Vendor || _currentUserService.AppUserType == AppUserTypeEnum.VendorAssociateUser,
//                    Name = command.Name,
//                    DisplayOrder = command.DisplayOrder,

//                };
//                _context.AppUserDepartments.Add(department);
//                await _context.SaveChangesAsync();

//            }
//            else
//            {
//                // Get department
//                department = await _context.AppUserDepartments
//                                   .FirstOrDefaultAsync(d => d.Id == command.Id);
//                if (department == null)
//                    throw new NotFoundException("Invalid department", "Department not found");
//                department.Description = command.Description;
//                department.Name = command.Name;
//                department.DisplayOrder = command.DisplayOrder;


//            }

//            // For limited to store, store must have Multiple stores feature 
//            if (command.LimitedToStores != null && command.LimitedToStores.LimitedToStores)
//            {
//                if (!await _featureService.HaveFeature(Data.Enumerations.AppFeaturesEnum.MultipleStores))
//                    throw new FeatureNotAvailableException(AppFeaturesEnum.MultipleStores.ToNameString());

//                department.LimitedToStores = command.LimitedToStores.LimitedToStores;

//                // Add store
//                foreach (var storeId in command.LimitedToStores.AddStores)
//                {
//                    var store = await _context.Stores
//                                 .Include(s => s.StoreMappings)
//                                 .FirstOrDefaultAsync(s => s.Id == storeId);
//                    if (store == null)
//                        throw new BadRequestException("Invalid store id in limited to store");

//                    if (store.StoreMappings.FirstOrDefault(m => m.EntityId == department.Id && m.EntityName == nameof(AppUserDepartment)) == null)
//                    {
//                        store.StoreMappings.Add(new Data.Entities.Stores.StoreMapping
//                        {
//                            EntityId = department.Id,
//                            EntityName = nameof(AppUserDepartment),
//                            StoreId = store.Id
//                        });
//                    }
//                }

//                // Remove store
//                foreach (var storeId in command.LimitedToStores.RemoveStores)
//                {
//                    var store = await _context.Stores
//                                 .Include(s => s.StoreMappings)
//                                 .FirstOrDefaultAsync(s => s.Id == storeId);
//                    if (store == null)
//                        throw new BadRequestException("Invalid store id in limited to store");
//                    var countryStoreMappings = store.StoreMappings
//                                            .Where(m => m.EntityId == department.Id && m.EntityName == nameof(AppUserDepartment));
//                    foreach (var map in countryStoreMappings)
//                    {
//                        map.IsDeleted = true;
//                    }
//                }
//            }

//            await _context.SaveChangesAsync();
//            return department.Id;
//        }

//        #endregion

//        #region Vendor Department

//        public async Task<int> InsertUpdateVendorAppUserDepartment(InsertUpdateVendorDepartment command)
//        {
//            //// Check for appUser type          

//            if (!_currentUserService.HavePermission(Permissions.VendorDepartmentManagement))
//                throw new ForbiddenException();

//            // New department
//            AppUserDepartment department;
//            if (command.Id == 0)
//            {
//                department = new AppUserDepartment
//                {
//                    Description = command.Description,
//                    LimitedToAppUserTypeId = (int)AppUserTypeEnum.Vendor,
//                    LimitedToStores = false,
//                    LimitedToVendors = true,
//                    Name = command.Name,
//                    DisplayOrder = command.DisplayOrder,

//                };
//                _context.AppUserDepartments.Add(department);
//                await _context.SaveChangesAsync();

//                // Vendor mapping
//                _context.VendorMappings.Add(new Data.Entities.Vendors.VendorMapping
//                {
//                    EntityId = department.Id,
//                    EntityName = nameof(AppUserDepartment),
//                    VendorId = command.VendorId
//                });


//            }
//            else
//            {
//                // Get department
//                department = await _context.AppUserDepartments
//                                   .FirstOrDefaultAsync(d => d.Id == command.Id);
//                if (department == null)
//                    throw new NotFoundException("Invalid department", "Department not found");
//                department.Description = command.Description;
//                department.Name = command.Name;
//                department.DisplayOrder = command.DisplayOrder;


//            }

//            await _context.SaveChangesAsync();
//            return department.Id;
//        }

//        public async Task<List<AppUserDepartmentDto>> VendorDepartments(int storeId, int vendorId)
//        {

//            if (!_currentUserService.HavePermission(Permissions.VendorDepartmentManagement))
//            {
//                if (vendorId != _currentUserService.VendorId)
//                    throw new ForbiddenException();
//            }

//            var departments = _context.AppUserDepartments
//                                 .AsNoTracking()
//                                 .Where(d => d.LimitedToAppUserTypeId == (int)AppUserTypeEnum.Vendor);

//            // Store mapping
//            departments = from c in departments
//                          join sm in _context.StoreMappings
//                        on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                          from sm in c_sm.DefaultIfEmpty()
//                          where !c.LimitedToStores || sm.StoreId == storeId
//                          select c;

//            // Vendor mapping
//            departments = from c in departments
//                          join sm in _context.VendorMappings
//                        on new { c1 = c.Id, c2 = nameof(AppUserDepartment) } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
//                          from sm in c_sm.DefaultIfEmpty()
//                          where !c.LimitedToVendors || sm.VendorId == vendorId
//                          select c;

//            return await departments.OrderBy(d => d.DisplayOrder)
//                                .Select(d => new AppUserDepartmentDto
//                                {
//                                    Description = d.Description,
//                                    DisplayOrder = d.DisplayOrder,
//                                    Id = d.Id,
//                                    Name = d.Name
//                                })
//                                .ToListAsync();
//        }

//        #endregion

//        public async Task<AppUserDepartmentDetailDto> AppUserDepartmentById(int id)
//        {
//            var department = await _context.AppUserDepartments
//                              .AsNoTracking()
//                              .FirstOrDefaultAsync(d => d.Id == id);

//            if (department == null)
//                throw new NotFoundException("Department not found");

//            // Check for limited to store
//            var limitedToStore = new LimitedToStoresDto
//            {
//                LimitedToStores = department.LimitedToStores,
//            };
//            if (department.LimitedToStores)
//            {
//                limitedToStore.Stores = await _context.StoreMappings
//                                .Where(s => s.EntityId == department.Id && s.EntityName == nameof(AppUserDepartment))
//                                .Select(s => s.StoreId)
//                                .ToListAsync();
//            }

//            return new AppUserDepartmentDetailDto
//            {
//                Description = department.Description,
//                DisplayOrder = department.DisplayOrder,
//                Id = department.Id,
//                Name = department.Name,
//                LimitedToStores = limitedToStore,
//            };
//        }

//        #endregion

//    }
//}
