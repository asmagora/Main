app.controller('APIController', function ($scope, APIService) {
    getAll('');

    function getAll(path) {
        var servCall = APIService.getAllFiles(path);
        servCall.then(function (d) {
            $scope.files = d.data;
            countSize(path);
        }, function (error) {
            $alert('Oops! Something went wrong while fetching the data.')
        })
    }

    function countSize(path) {
        var servCall = APIService.getFilesCount(path);
        servCall.then(function (d) {
            $scope.sizes = d.data;
        }, function (error) {
            $alert('Oops! Something went wrong while fetching the data.')
        })
    }

    $scope.goSomewhere = function (path) {
        getAll(path)
    };
})