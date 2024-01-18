'use strict';

(function () {





    var app = angular.module("LaserAfzarServiceViewer", []);

    app.filter('split', function () {
        return function (input, splitChar, splitIndex) {
            // do some bounds checking here to ensure it has that index
            return input.split(splitChar)[splitIndex];
        }
    });

    app.directive('myPostRepeatDirective', function () {
        return function (scope, element, attrs) {
            if (scope.$last) {
                // iteration is complete, do whatever post-processing
                // is necessary
                myFunc();
            }
        };
    });

    app.directive('myNextRepeatDirective', function () {
        return {
            link: function ($scope, element, attrs) {
                // Trigger when number of children changes,
                // including by directives like ng-repeat
                var watch = $scope.$watch(function () {
                    return element.children().length;
                }, function () {
                    // Wait for templates to render
                    $scope.$evalAsync(function () {
                        // Finally, directives are evaluated
                        // and templates are renderer here
                        //var children = element.children();
                        //console.log(children);
                        myNanoGalleryFunc();
                    });
                });
            },
        };
    });

    var ServiceViewerController = function ($scope, $http, $sce) {


        var onComplete = function (response) {
            $scope.ServiceDetail = response.data;
            $scope.trustAsHtml = $sce.trustAsHtml;
        };

        var onError = function (reason) {
            $scope.ServiceDetailsError = "There is an error within retreaving the information";
        };

        $scope.$watch("serviceId", function () {

            $http.get("/api/ServicesApi/" + $scope.serviceId)
                  .then(onComplete, onError);

        });

    };

    app.controller("ServiceViewerController", ["$scope", "$http", "$sce", ServiceViewerController]);

}());