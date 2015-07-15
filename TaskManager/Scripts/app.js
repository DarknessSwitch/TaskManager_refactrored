var ViewModel = function () {
    var self = this;
    self.tasks = ko.observableArray();
    self.IsTaskListVisible = ko.observable(false);
    self.subtasks = ko.observableArray();
    self.subtaskText = ko.observable("");
    self.taskText = ko.observable("");
    self.taskDate = ko.observable();
    var tasksUri = '/api/task/';
    var subtasksUri = '/api/subtask/';

    var ajaxHelper = function (uri, method, data) {
        var request = {
            url: uri,
            type: method,
            contentType: "application/json",
            accepts: "application/json",
            cache: false,
//            dataType: 'json',
            data: JSON.stringify(data),
            error: function (jqXHR) {
                console.log("ajax error " + jqXHR.status);
            }
        };
        return $.ajax(request);
    };

    function getAllTasks() {
        ajaxHelper(tasksUri, 'GET').done(function (data) { self.tasks(data); });
        for (var i = 0; i < self.tasks().length; i++) {
            self.tasks()[i] = {
                IsDone: ko.observable(self.tasks()[i].IsDone)
            }
        }
    }

    function getAllSubtasks() {
        ajaxHelper(subtasksUri, 'GET').done(function (data) { self.subtasks(data); });
    }

    getAllTasks();
    getAllSubtasks();

    self.removeTask = function (task) {
        ajaxHelper(tasksUri+"/delete/" + task.Id, 'DELETE').done(function (data) {
            self.tasks.remove(task);
        });
    }

    self.removeDoneTasks = function () {
        for (var i = 0; i < self.tasks().length; i++) {
            if (self.tasks()[i].IsDone)
                self.removeTask(self.tasks()[i]);
        }
    }

    self.changeVisibleState = function () {
        if (!self.IsTaskListVisible())
            self.IsTaskListVisible(true);
        else
            self.IsTaskListVisible(false);
    }

    self.addTask = function () {
        var task = {
            Text: self.taskText(),
            Date: self.taskDate(),
            IsDone: false
//            CategoryUrl: "/Home/Category/1"
        }
        ajaxHelper(tasksUri+"/post", 'POST', task).done(function (item) { self.tasks.push(item); });
    }

    self.addSubtask = function (task) {
        var subtask = {
            Text: self.subtaskText(),
            TaskId: task.Id
        };
        ajaxHelper(subtasksUri + "/post",  'POST', subtask).done(function (item) {
            self.subtasks.push(item);
        });
    }
    self.markTaskDone = function (task) {
        var i = self.tasks.indexOf(task);
        task.IsDone = true;
        ajaxHelper(tasksUri+"/put/" + task.Id, 'PUT', task).done(function (res) { self.updateTask(self.tasks()[i], task); });
    }
    self.markTaskUndone = function (task) {
        var i = self.tasks.indexOf(task);
        task.IsDone = false;
        ajaxHelper(tasksUri + "/put/" + task.Id, 'PUT', task).done(function (res) { self.updateTask(self.tasks()[i], task); });
    }
    self.changeTaskState = function (task) {
        if (task.IsDone) {
            self.markTaskUndone(task);
        }
        else {
            self.markTaskDone(task);
        }
    }
    self.updateTask = function (task, newTask) {
        var i = self.tasks.indexOf(task);
        self.tasks()[i].Text = newTask.Text;
        self.tasks()[i].IsDone = newTask.IsDone;
        self.tasks()[i].Date = newTask.Date;
        self.tasks()[i].CategoryId = newTask.CategoryId;
//        self.tasks()[i].CategoryUrl = newTask.CategoryUrl;
    }
    self.updateSubtask = function (subtask, newSubtask) {
        var i = self.subtasks.indexOf(subtask);
        self.subtasks()[i].Text = newSubtask.Text;
        self.subtasks()[i].IsDone = newSubtask.IsDone;
        self.subtasks()[i].TaskId = newSubtask.TaskId;
    }

    function checkIfSubtasksDone(task) {
        var b = true;
        for (i = 0; i < self.subtasks().length; i++)
            if (self.subtasks()[i].TaskId == task.Id)
                if (!self.subtasks()[i].IsDone)
                    b = false;
        return b;
    }

    self.changeSubtaskState = function (subtask) {
        var i = self.subtasks().indexOf(subtask);
        var k;
        for (j = 0; j < self.tasks().length; j++)
            if (self.tasks()[j].Id == subtask.TaskId)
                k = j;
        var task = self.tasks()[k];

        if (subtask.IsDone) {
            subtask.IsDone = false;
            ajaxHelper(subtasksUri+"/put/" + subtask.Id, 'PUT', subtask).done(function (res) { self.updateSubtask(self.subtasks()[i], subtask); });
        }
        else {
            subtask.IsDone = true;
            ajaxHelper(subtasksUri+"/put/" + subtask.Id, 'PUT', subtask).done(function (res) { self.updateSubtask(self.subtasks()[i], subtask); });
//            if (checkIfSubtasksDone(task))
//                self.markTaskDone(task);
        }
    }

    self.removeSubtask = function (subtask) {
        ajaxHelper(subtasksUri+"/delete/" + subtask.Id, 'DELETE').done(function (data) {
            self.subtasks.remove(subtask);
        });
    }
};
ko.applyBindings(new ViewModel());
