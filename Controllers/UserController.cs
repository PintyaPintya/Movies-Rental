using Microsoft.AspNetCore.Mvc;
using MoviesRental.IRepository;
using MoviesRental.Models;
using MoviesRental.Models.Dto;
using MoviesRental.Repository;
using System.Threading.Tasks;

namespace MoviesRental.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsers();
        return View(users);
    }

    public IActionResult Add()
    {
        return View("AddUpdate");
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddUserDto addUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(addUserDto);
            }

            bool ifEmailExists = await _userRepository.CheckIfEmailExists(addUserDto.EmailAddress);
            if (ifEmailExists)
            {
                ModelState.AddModelError("EmailAddress", "Email already exists");
            }

            var user = new User()
            {
                Name = addUserDto.Name,
                EmailAddress = addUserDto.EmailAddress,
                Password = PasswordHasher.Encode(addUserDto.Password),
                BirthDate = addUserDto.BirthDate
            };
            await _userRepository.Add(user);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();

            var userDto = new AddUserDto()
            {
                Name = user.Name,
                EmailAddress = user.EmailAddress,
                BirthDate = user.BirthDate,
                Password = user.Password
            };

            return View("AddUpdate", userDto);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, AddUserDto addUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(addUserDto);
            }

            bool ifEmailExists = await _userRepository.CheckIfEmailExists(addUserDto.EmailAddress);
            if (ifEmailExists)
            {
                ModelState.AddModelError("EmailAddress", "Email already exists");
            }

            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();

            user.Name = addUserDto.Name;
            user.EmailAddress = addUserDto.EmailAddress;
            user.BirthDate = addUserDto.BirthDate;

            await _userRepository.Update(user);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();

            await _userRepository.Delete(user);

            return NoContent();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }
}
