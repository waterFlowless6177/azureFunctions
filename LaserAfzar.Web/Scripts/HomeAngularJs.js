
(function () {

    var app = angular.module("LaserAfzarViewer", []);


    var AboutController = function ($scope, $http, $sce) {

        var onComplete = function (response) {
            $scope.AboutUsArray = response.data;
            //$scope.item = $sce.trustAsHtml('<h1>Some HTML code</h1>');
            $scope.trustAsHtml = $sce.trustAsHtml;
        };

        var onError = function (reason) {
            $scope.aboutError = "An error occured retreaving about!";
        }

        $http.get("/api/AboutUsApi")
             .then(onComplete, onError);

    };

    var FeatureController = function ($scope, $http) {

        var onComplete = function (response) {
            $scope.FeturesArray = response.data;
        };

        var onError = function (reason) {
            $scope.featureError = "An error occured retreaving Fetures!";
        };


        $http.get("api/FeaturesApi")
             .then(onComplete, onError);
    };

    var MemberController = function ($scope, $http) {

        var onComplete = function (response) {
            $scope.MemberArray = response.data;
        };

        var onError = function (reason) {
            $scope.memberError = "An error occured during retreaving memebers information!";
        };

        $http.get("/api/MembersApi")
             .then(onComplete, onError);

    };

    var ServiceController = function ($scope, $http, $sce) {

        var onComplete = function (response) {
            $scope.ServiceArray = response.data;
            $scope.trustAsHtml = $sce.trustAsHtml;
        };

        var onError = function (reason) {
            $scope.serviceError = "An error occured during retreaving services information!";
        };

        $http.get("/api/ServicesApi")
             .then(onComplete, onError);

    };



    app.controller("AboutController", ["$scope", "$http", "$sce", AboutController]);
    app.controller("FeatureController", ["$scope", "$http", FeatureController]);
    app.controller("MemberController", ["$scope", "$http", MemberController]);
    app.controller("ServiceController", ["$scope", "$http", "$sce", ServiceController]);


}());
