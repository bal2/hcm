export class RoleModel {
    roleId: number;
    name: string;
    description: string;
}

export class NewRoleModel {
    name: string;
    description: string;
}

export class RoleUserModel {
    userId: number;
    firstName: string;
    lastName: string;
    title: string;
}

export class PermissionModel {
    permissionId: number;
    name: string;
    description: string;
    inRole: boolean; //Set in GUI if permission is part of selected role
}