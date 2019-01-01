export class GroupModel {
    groupId: number;
    name: string;
    shortName: string;
    description: string;
    createdAt: Date;
    updatedAt: Date;
}

export class NewGroupModel {
    name: string;
    shortName: string;
    description: string;
}

export class GroupMemberModel {
    userId: number;
    firstName: string;
    lastName: string;
    title: string;
}

export class GroupMemberDetailsModel {
    userId: number;
    firstName: string;
    lastName: string;
    title: string;
    base64Picture: string;
}