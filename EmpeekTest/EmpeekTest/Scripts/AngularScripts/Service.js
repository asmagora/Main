app.service("APIService", function ($http) {
    this.getAllFiles = function (path) {
        var result = $http.get("api/web/GetObjectsInCurrentFolder", { params: { PathToFile: path } });
        return result;
    }

    this.getFilesCount = function (path) {
        var result = $http.get("api/web/GetFilesCountInCurrentFolder", { params: { PathToFile: path } });
        return result;
    }
});