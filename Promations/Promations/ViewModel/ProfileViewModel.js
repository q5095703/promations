
function promationItem(promation) {
    this.ID = ko.observable(promation.id);
    if (promation.product) {
        this.ProductName = ko.observable(promation.product.name);
    }
    else {
        this.ProductName = ko.observable();
    }
    this.Start = ko.observable(moment(promation.Start).format('YYYY-MM-DD'));

    if (promation.productID === "") {
        this.canChange = ko.observable(true);
    } else {
        this.canChange = ko.observable(false);
    }

    this.End = ko.observable(moment(promation.End).format('YYYY-MM-DD'));
    this.ProductID = ko.observable(promation.productID);
    this.Product = ko.observable();
}
function productItem(product) {

    this.ID = ko.observable(product.id);
    this.ProductName = ko.observable(product.name);
}


function dateFormmatter(item) {
    var date = new Date(item);
    var day = date.getDate();       // yields date
    var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    var year = date.getFullYear();  // yields year
    var hour = date.getHours();     // yields hours 
    var minute = date.getMinutes(); // yields minutes
    var second = date.getSeconds(); // yields seconds

    // After this construct a string with the above results as below
    var time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second;

    return time;
}

var viewModel = function () {

    var self = this;
    self.load = ko.observable(false);
    self.model = {};
    self.model.Promations = ko.observableArray();
    self.model.Products = ko.observableArray();
    self.CanEditPromations = ko.observable(false);
    self.currentSelected = ko.observable(new promationItem({
        "id": -1,
        "start": "",
        "end": "",
        "productID": ""

    }));
    self.currentProduct = ko.observable();
    self.addPromation = function () {


        self.CanEditPromations(true);

        self.currentSelected(new promationItem({
            "id": -1,
            "start": "",
            "end": ""
        }));
    }
    self.editPromation = function (item) {

        self.currentSelected(item);
        self.CanEditPromations(true);
        self.currentProduct(ko.utils.arrayFirst(self.model.Products(), function (data) { return item.ProductID() === data.ID() }));

    }



    self.deletePromation = function (item) {

        $.ajax({
            type: "post",
            url: "/Promations/Delete",
            data: { "ID": item.ID },
            dataType: "json",
            sucess: function (data) {
                self.CanEditPromations(false);
            }
        })
    }
    self.UpdatePromation = function (item) {

        var uncomplete = false;

        if (moment(item.Start()) < moment()) {
            uncomplete = true;
            $("#promation-start").addClass("input--red");
        }
        if (item.Start() === "") {
            uncomplete = true;
            $("#promation-start").addClass("input--red");
        } else {
            item.Start(dateFormmatter(item.Start()));
        }
        if (item.End() === "") {
            uncomplete = true;
            $("#promation-end").addClass("input--red");
        } else {

            item.End(dateFormmatter(item.End()));
        }
        if (item.Start() > item.End()) {
            uncomplete = true;
            $("#promation-start").addClass("input--red");
            $("#promation-end").addClass("input--red");
        }
        item.ProductID(self.currentProduct().ID());

        if (uncomplete === false) {
            if (self.model.Promations.indexOf(item) >= 0) {
                $.ajax({
                    type: "post",
                    url: "/Promations/Edit",
                    data: { "promation": item },
                    dataType: "json",
                    sucess: function (data) {
                        self.CanEditAddress(false);
                    }
                })
            } else {
                $.ajax({
                    type: "post",
                    url: "/Promations/Create",
                    data: { "promation": item },
                    dataType: "json",
                    sucess: function (data) {
                        self.model.Addresses.push(item);
                        self.CanEditAddress(false);
                    }
                });
            }

        }
        // else {

        // }
    }

}




$(function () {
    var vm = new viewModel();


    $.getJSON("/Home/Products", function (Products) {
        vm.model.Products.removeAll();
        if (Products.length !== 0) {
            $.each(Products, function (index, item) {
                vm.model.Products.push(new productItem(item));
            });
        }
        Promations();
    })


    function Promations() {
        $.getJSON("/Promations/Promations", function (Promations) {

            vm.model.Promations.removeAll();
            if (Promations.length !== 0) {
                $.each(Promations, function (index, item) {

                    vm.model.Promations.push(new promationItem(item));
                });
            }
            tryApply();
        })
    }



    function tryApply() {
        vm.load(true);
        ko.applyBindings(vm);
    }


})
