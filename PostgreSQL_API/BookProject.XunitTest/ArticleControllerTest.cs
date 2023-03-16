using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Application.Models;
using BookProject.Application.Services;
using BookProject.Controllers;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BookProject.XunitTest
{
    //public class articlecontrollertest
    //{
    //    private mock<ıarticlerepository> _mock;
    //    private mock<ımagazinerepository> _mockmagazine;
    //    private mock<ıuserrepository> _mockuser;
    //    private readonly articleservice _articleservice;
    //    private readonly userservice _userservice;
    //    private readonly magazineservice _magazineservice;
    //    private readonly articlecontroller _articlecontroller;
    //    ımapper mapper = bookprojectmapper.mapper;
    //    public articlecontrollertest()
    //    {
    //        _mock = new mock<ıarticlerepository>();
    //        _mockmagazine = new mock<ımagazinerepository>();
    //        _mockuser = new mock<ıuserrepository>();
    //        _articleservice = new articleservice(_mock.object, mapper);
    //        _magazineservice = new magazineservice(_mockmagazine.object);
    //        _userservice = new userservice(_mockuser.object);
    //        _articlecontroller = new articlecontroller(_userservice, _articleservice, _magazineservice);
    //    }
    //    //[fact]
    //    //public void get_returns_correct_ıd()
    //    //{
    //    //    // arrange
    //    //    var articletoget = fakedata();
    //    //    _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //    //    // act
    //    //    var actionresult = _articlecontroller.get(1);
    //    //    var okobjectresult = actionresult.result as okobjectresult;

    //    //    // assert
    //    //    var articlemap = mapper.map<articlemodel>(articletoget);
    //    //    assert.ıstype<okobjectresult>(okobjectresult);


    //    //}
    //    //[fact]
    //    //public void get_returns_wrong_ıd()
    //    //{
    //    //    // arrange
    //    //    _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //    //    // act
    //    //    var actionresult = _articlecontroller.get(2);
    //    //    var okobjectresult = actionresult.result as notfoundresult;
    //    //    // assert
    //    //    assert.equal(okobjectresult.statuscode, (int)httpstatuscode.notfound);

    //    //}
    //    [fact]
    //    public void get_returns_negative_ıd()
    //    {
    //        // arrange
    //        _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //        // act
    //        var actionresult = _articlecontroller.get(0);
    //        var badobjectresult = actionresult.result as badrequestobjectresult;

    //        // assert
    //        assert.equal(badobjectresult.statuscode, (int)httpstatuscode.badrequest);
    //    }
    //    [fact]
    //    public void getall_returns_correctly()
    //    {
    //        // arrange
    //        _mock.setup(y => y.getallasync());
    //        // act
    //        var actionresult = _articlecontroller.getall();
    //        var okobjectresult = actionresult.result as okobjectresult;

    //        // assert
    //        assert.equal(okobjectresult.statuscode, (int)okobjectresult.statuscode);

    //    }
    //    [fact]
    //    public async task create_return_correctly()
    //    {
    //        // arrange
    //        var added = fakedata();
    //        var articlemodel = mapper.map<articlemodel>(added);

    //        _mockuser.setup(x => x.getbyıdasync(articlemodel.authorıd))
    //                        .returnsasync(new user());

    //        _mockmagazine.setup(x => x.getbyıdasync(articlemodel.magazineıd))
    //                            .returnsasync(new magazine());

    //        _mock.setup(x => x.getbyıdasync(articlemodel.id))
    //                           .returnsasync(null as article);

    //        _mock.setup(x => x.addasync(added))
    //                           .returnsasync(added);

    //        // act
    //        var actionresult = _articlecontroller.create(articlemodel);
    //        var okobjectresult = actionresult.result as okobjectresult;

    //        // assert
    //        assert.ıstype<okobjectresult>(okobjectresult);
    //    }

    //    [fact]
    //    public void update_return_correctly()
    //    {

    //        // arrange
    //        var articletoupdate = fakedata();
    //        articletoupdate.content = "this is updated content";
    //        _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //        _mock.setup(y => y.updateasync(articletoupdate)).returnsasync(articletoupdate);
    //        articlemodel updatedarticlemodel = new articlemodel { id = 1, content = "this is updated content" };

    //        // act
    //        var actionresult = _articlecontroller.update(1, updatedarticlemodel);
    //        var okobjectresult = actionresult.result as okobjectresult;

    //        // assert
    //        assert.ıstype<okobjectresult>(okobjectresult);


    //    }
    //    [fact]
    //    public void delete_return_correctly()
    //    {
    //        // arrange
    //        _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //        // act
    //        var actionresult = _articlecontroller.delete(1).result;

    //        // assert
    //        assert.ıstype<okobjectresult>(actionresult);

    //    }
    //    [fact]
    //    public void delete_returns_notfound_ınvalidıd()
    //    {
    //        // arrange
    //        _mock.setup(y => y.getbyıdasync(1)).returnsasync(fakedata());
    //        // act
    //        var actionresult = _articlecontroller.delete(4).result;
    //        // assert
    //        assert.ıstype<badrequestobjectresult>(actionresult);
    //    }
    //    public article fakedata()
    //    {
    //        return new article
    //        {
    //            ıd = 1,
    //            title = "test",
    //            content = "test",
    //            magazineıd = 1,
    //            authorıd = 1
    //        };
    //    }
    //}
}
