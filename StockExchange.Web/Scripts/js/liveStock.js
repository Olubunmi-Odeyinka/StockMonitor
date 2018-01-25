

(function () {
    'use strict';

    window.app.factory("testService",
    [
        '$soap', function($soap) {
            var base_url = "http://localhost:49274/Service.asmx";

            return {
                LiveStock: function (token) {
                    return $soap.post(base_url, "LiveStock", { token: token });
                }
            }
        }
    ]);

    window.app.controller('MainCtrl',
        function ($scope, testService, $interval) {
            $scope.token = "";
            $scope.Error = false;
            var getStocks = function () {
                testService.LiveStock($scope.token).then(function (response) {

                    if (response === "You are not authorized to call this Service" ||
                        response === "Your token is no longer valid reload your page ") {
                        $scope.response = response;
                        $scope.Error = true;
                    }

                    $scope.Error = false;
                    var jsonResult = {};
                    if (!(response ==="" || response === null || response === undefined)) {
                        jsonResult = JSON.parse(response);
                    }

                    $scope.response = jsonResult.Data;
                    $scope.token = jsonResult.AuthToken;
                });
            }

            $scope.setToken = function(tokenString) {
                $scope.token = tokenString;
                getStocks();
            }

            $interval(function () {
                getStocks();
            }, 10000);

            
        });
})();