export class ListResponse<T> {
    paging: Paging;

    links: LinkInfo[];

    items: T[];
}

export class Paging {
    totalItems: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}

export class LinkInfo {
    href: string;
    rel: string;
    methid: string;
}